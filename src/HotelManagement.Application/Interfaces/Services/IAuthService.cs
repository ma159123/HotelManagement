using HotelManagement.Application.DTOs.Auth;
using HotelManagement.Application.Features.Auth.Commands.Login;
using HotelManagement.Application.Features.Auth.Commands.Register;
using HotelManagement.Domain.Common;

namespace HotelManagement.Application.Interfaces.Services
{
    public interface IAuthService
    {
        public Task<Result<RegisterResponse>> RegisterAsync(
         RegisterCommand command, CancellationToken cancellationToken);

        public Task<Result<LoginResponse>> LoginAsync(
              LoginCommand command, CancellationToken cancellationToken);

        public Task<Result<TokenResponse>> RefreshTokenAsync(
              string refreshToken, CancellationToken cancellationToken);
    }
}
