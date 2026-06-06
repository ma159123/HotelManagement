using HotelManagement.Application.DTOs.Auth;
using HotelManagement.Application.Interfaces.Services;
using HotelManagement.Domain.Common;
using MediatR;

namespace HotelManagement.Application.Features.Auth.Commands.Register
{
    public class RegisterCommandHandler
     : IRequestHandler<RegisterCommand, Result<RegisterResponse>>
    {
        private readonly IAuthService _authService;

        public RegisterCommandHandler(IAuthService authService)
            => _authService = authService;

        public async Task<Result<RegisterResponse>> Handle(
            RegisterCommand request,
            CancellationToken cancellationToken)
        {
            return await _authService.RegisterAsync(request, cancellationToken);
        }
    }

}
