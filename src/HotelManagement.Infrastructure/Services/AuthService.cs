using HotelManagement.Application.DTOs.Auth;
using HotelManagement.Application.Features.Auth.Commands.Login;
using HotelManagement.Application.Features.Auth.Commands.Register;
using HotelManagement.Application.Interfaces.Services;
using HotelManagement.Domain.Common;
using HotelManagement.Domain.Common.Errors;
using HotelManagement.Domain.Entities;
using HotelManagement.Domain.Enums;
using HotelManagement.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace HotelManagement.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        public AuthService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IEmailService emailService,
        IConfiguration configuration,
        ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _configuration = configuration;
            _context = context;
        }


        // ─── Register ───────────────────────────────

        public async Task<Result<RegisterResponse>> RegisterAsync(RegisterCommand command, CancellationToken cancellationToken)
        {
            // 1 - check email
            var existingUser = await _userManager.FindByEmailAsync(command.Email);
            if (existingUser != null)
                return Result.Failure<RegisterResponse>(Error.AlreadyElementExist);

            // 2 - create Guest object
            var guest = new Guest
            {
                FullName = command.FullName,
                Email = command.Email,
                UserName = command.Email,
                PhoneNumber = command.Phone,
                Address = command.Address,
                CreatedAt = DateTime.UtcNow
            };
            // 3 - Create User 
            var result = await _userManager.CreateAsync(guest, command.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return Result.Failure<RegisterResponse>(new Error("Identity.Error", errors, 400));
            }
            await _userManager.AddToRoleAsync(guest, RoleEnum.Guest.ToString());
            // 4 - generate Email Confirmation Token
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(guest);
            // 3. Encode  Token (have special characters)
            var encodedToken = WebEncoders.Base64UrlEncode(
                Encoding.UTF8.GetBytes(token));

            // 4. اعمل Confirmation Link
            var confirmationLink =
                $"https://localhost:7028/api/auth/confirm-email" +
                $"?email={guest.Email}&token={encodedToken}";

            // 5. ابعت الإيميل
            await _emailService.SendConfirmationEmailAsync(
                guest.Email!,
                guest.FullName,
                confirmationLink,
                cancellationToken);
            return Result.Success(new RegisterResponse(
            guest.Id,
            guest.FullName,
            guest.Email,
            "Registration successful. Please check your email to confirm your account."
        ));
        }

        // ─── Login ───────────────────────────────
        public async Task<Result<LoginResponse>> LoginAsync(LoginCommand command, CancellationToken cancellationToken)
        {
            // 1 - get user
            var guest = await _userManager.FindByEmailAsync(command.Email);
            if (guest == null)
                return Result.Failure<LoginResponse>(Error.NotFound);

            // 2 - check pass Lockout
            var result = await _signInManager.CheckPasswordSignInAsync(
                guest, command.Password, lockoutOnFailure: true);

            if (result.IsLockedOut)
                return Result.Failure<LoginResponse>(
                    new Error("Auth.LockedOut", "Account is locked. Try again later.", 403));

            if (result.IsNotAllowed)
                return Result.Failure<LoginResponse>(
                    new Error("Auth.EmailNotConfirmed", "Please confirm your email first.", 403));

            if (!result.Succeeded)
                return Result.Failure<LoginResponse>(
                    new Error("Auth.InvalidCredentials", "Invalid email or password.", 401));
            var roles = await _userManager.GetRolesAsync(guest);
            // 3 - generate Access Token
            var accessToken = GenerateAccessToken(guest, roles);
            // 4 - generate Refresh Token 
            var refreshToken = await GenerateRefreshTokenAsync(guest.Id, cancellationToken);

            return Result.Success(new LoginResponse(
            guest.Id,
            guest.FullName,
            guest.Email!,
            accessToken.Token,
            refreshToken,
            accessToken.Expiry
        ));
        }

        // ─── Token ───────────────────────────────
        public Task<Result<TokenResponse>> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        // ─── Generate Access Token ───────────────────
        private (string Token, DateTime Expiry) GenerateAccessToken(ApplicationUser guest, IList<string> roles)
        {
            var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, guest.Id),
            new(ClaimTypes.Email, guest.Email!),
            new(ClaimTypes.Name, guest.FullName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
            // add Roles 
            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

            var expiry = DateTime.UtcNow.AddMinutes(
                int.Parse(_configuration["Jwt:AccessTokenExpiryMinutes"]!));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiry,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return (new JwtSecurityTokenHandler().WriteToken(token), expiry);
        }

        // ─── Generate Refresh Token ──────────────────
        private async Task<string> GenerateRefreshTokenAsync(
            string guestId, CancellationToken cancellationToken)
        {
            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

            var refreshToken = new RefreshToken
            {
                GuestId = guestId,
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddDays(
                    int.Parse(_configuration["Jwt:RefreshTokenExpiryDays"]!)),
                CreatedAt = DateTime.UtcNow
            };

            await _context.RefreshTokens.AddAsync(refreshToken, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return token;
        }
    }
}
