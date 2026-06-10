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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;
        public ResendConfirmationEmailHandler(UserManager<ApplicationUser> userManager, IEmailService emailService)
        {
            this._userManager = userManager;
            this._emailService = emailService;
        }
        public async Task<Result<string>> Handle(
     ResendEmailConfirmationCommand request,
     CancellationToken cancellationToken)
        {
            // 1.get guest
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return Result.Failure<string>(Error.NotFound);

            // 2 - generate Email Confirmation Token
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            // 3. Encode  Token (have special characters)
            var encodedToken = WebEncoders.Base64UrlEncode(
                Encoding.UTF8.GetBytes(token));

            // 4.  Confirmation Link
            var confirmationLink =
                $"https://localhost:7028/api/auth/confirm-email" +
                $"?email={user.Email}&token={encodedToken}";

            // 5. ابعت الإيميل
            await _emailService.SendConfirmationEmailAsync(
                user.Email!,
                user.FullName,
                confirmationLink,
                cancellationToken);

            return Result.Success("Email confirmation link sent successfully. Check you email.");
        }
    }
}
