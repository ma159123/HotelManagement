using HotelManagement.Domain.Common;
using HotelManagement.Domain.Common.Errors;
using HotelManagement.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace HotelManagement.Application.Features.Auth.Commands.ConfirmEmail
{
    internal class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, Result<string>>
    {
        private readonly UserManager<Guest> _userManager;
        public ConfirmEmailCommandHandler(UserManager<Guest> userManager)
        {
            this._userManager = userManager;
        }
        public async Task<Result<string>> Handle(
     ConfirmEmailCommand request,
     CancellationToken cancellationToken)
        {
            // 1.get guest
            var guest = await _userManager.FindByEmailAsync(request.Email);
            if (guest == null)
                return Result.Failure<string>(Error.NotFound);

            // 2. Decode  Token
            var decodedToken = Encoding.UTF8.GetString(
                WebEncoders.Base64UrlDecode(request.Token));

            // 3. confirm email
            var result = await _userManager
                .ConfirmEmailAsync(guest, decodedToken);

            if (!result.Succeeded)
                return Result.Failure<string>(
                    new Error("Auth.InvalidToken", "Invalid or expired token.", 400));

            return Result.Success("Email confirmed successfully. You can now login.");
        }
    }
}
