using HotelManagement.Application.DTOs.Auth;
using HotelManagement.Application.Interfaces.Services;
using HotelManagement.Domain.Common;
using MediatR;

namespace HotelManagement.Application.Features.Auth.Commands.Login
{
    public class LoginCommandHandler
     : IRequestHandler<LoginCommand, Result<LoginResponse>>
    {
        private readonly IAuthService _authService;

        public LoginCommandHandler(IAuthService authService)
            => _authService = authService;

        public async Task<Result<LoginResponse>> Handle(
            LoginCommand request,
            CancellationToken cancellationToken)
        {
            return await _authService.LoginAsync(request, cancellationToken);
        }
    }

}
