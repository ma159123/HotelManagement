using HotelManagement.Application.Interfaces.Services;
using HotelManagement.Domain.Common;
using HotelManagement.Domain.Common.Errors;
using HotelManagement.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace HotelManagement.Application.Features.Auth.Commands.resend_confirmation_email
{
    public class ResendConfirmationEmailHandler : IRequestHandler<ResendEmailConfirmationCommand, Result<string>>
    {
        private readonly ITokenCache _tokenCache;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;
        public ResendConfirmationEmailHandler(UserManager<ApplicationUser> userManager, IEmailService emailService, ITokenCache tokenCache)
        {
            this._userManager = userManager;
            this._emailService = emailService;
            _tokenCache = tokenCache;
        }
        public async Task<Result<string>> Handle(
     ResendEmailConfirmationCommand request,
     CancellationToken cancellationToken)
        {
            // 1.get guest
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return Result.Failure<string>(Error.NotFound);
            if (user.EmailConfirmed)
                return Result.Failure<string>(Error.AlreadyEmailConfirmed);

            // Check rate limit (separate cache key)
            var rateLimitKey = $"rate_limit:{user.Id}";
            var rateLimit = await _tokenCache.GetAsync(rateLimitKey);
            if (rateLimit != null)
                return Result.Failure<string>(Error.TooManyRequests);

            //  - generate Email Confirmation Token
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            // . Encode  Token (have special characters)
            var encodedToken = WebEncoders.Base64UrlEncode(
                Encoding.UTF8.GetBytes(token));
            // Save in Redis with 5 min expiry
            var cacheKey = $"email_confirm:{user.Id}";
            await _tokenCache.SetAsync(cacheKey, encodedToken, TimeSpan.FromMinutes(5));

            // Set rate limit (1 min between requests)
            await _tokenCache.SetAsync(rateLimitKey, "1", TimeSpan.FromMinutes(1));
            // .  Confirmation Link
            var confirmationLink =
                $"https://localhost:7028/api/auth/confirm-email" +
                $"?email={user.Email}&token={encodedToken}";

            // . send email
            await _emailService.SendConfirmationEmailAsync(
                user.Email!,
                user.FullName,
                confirmationLink,
                cancellationToken);

            return Result.Success("Email confirmation link sent successfully. Check you email.");
        }
    }
}
