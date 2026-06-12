using HotelManagement.Application.Features.Auth.Commands.ChangePassword;
using HotelManagement.Application.Features.Auth.Commands.Login;
using HotelManagement.Application.Features.Auth.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.API.Controllers
{
    // API/Controllers/AuthController.cs
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ISender _mediator;

        public AuthController(ISender mediator)
            => _mediator = mediator;

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsFailure)
                return StatusCode(result.Error.StatusCode!.Value, result.Error);
            return Ok(result.Value);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsFailure)
                return StatusCode(result.Error.StatusCode!.Value, result.Error);
            return Ok(result.Value);
        }

        [HttpPatch("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsFailure)
                return StatusCode(result.Error.StatusCode!.Value, result.Error);
            return Ok(result.Value);
        }
    }
}
