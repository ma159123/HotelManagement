using HotelManagement.Application.Interfaces.Services;
using HotelManagement.Domain.Common;
using HotelManagement.Domain.Common.Errors;
using HotelManagement.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace HotelManagement.Application.Features.Auth.Commands.ConfirmEmail
{
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, Result<string>>
    {
        private readonly ITokenCache _tokenCache;
        private readonly UserManager<ApplicationUser> _userManager;
        public ConfirmEmailCommandHandler(UserManager<ApplicationUser> userManager, ITokenCache tokenCache)
        {
            this._userManager = userManager;
            _tokenCache = tokenCache;
        }
        public async Task<Result<string>> Handle(
     ConfirmEmailCommand request,
     CancellationToken cancellationToken)
        {
            // .get guest
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return Result.Failure<string>(Error.NotFound);

            // Get token from Redis
            var cacheKey = $"email_confirm:{user.Id}";
            var cachedToken = await _tokenCache.GetAsync(cacheKey);

            if (cachedToken == null || cachedToken != request.Token)
                return Result.Failure<string>(new Error("Auth.InvalidToken", "Invalid or expired token.", 400));
            // . Decode  Token
            var decodedToken = Encoding.UTF8.GetString(
                WebEncoders.Base64UrlDecode(request.Token));

            // . confirm email
            var result = await _userManager
                .ConfirmEmailAsync(user, decodedToken);

            if (!result.Succeeded)
                return Result.Failure<string>(
                    new Error("Auth.InvalidToken", "Invalid or expired token.", 400));

            // Remove from cache
            await _tokenCache.RemoveAsync(cacheKey);

            return Result.Success("Email confirmed successfully. You can now login.");
        }
    }
}
