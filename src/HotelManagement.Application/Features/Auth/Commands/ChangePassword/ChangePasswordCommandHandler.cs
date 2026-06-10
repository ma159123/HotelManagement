using HotelManagement.Domain.Common;
using HotelManagement.Domain.Common.Errors;
using HotelManagement.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace HotelManagement.Application.Features.Auth.Commands.ChangePassword
{
    internal class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public ChangePasswordCommandHandler(UserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;
        }
        public async Task<Result<string>> Handle(
         ChangePasswordCommand request,
         CancellationToken cancellationToken)
        {
            // 1.get guest
            var user = await _userManager.FindByIdAsync(request.Id);
            if (user == null)
                return Result.Failure<string>(Error.NotFound);

            // 2.Change pass
            var result = await _userManager
                .ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

            if (!result.Succeeded)
                return Result.Failure<string>(
                    new Error("Auth.InvalidToken", "Invalid or expired token.", 400));

            return Result.Success("Password changed successfully. You can now login with new password.");
        }
    }
}
