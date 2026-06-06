using HotelManagement.Application.Features.Auth.Commands.ConfirmEmail;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly ISender _mediator;

        public EmailController(ISender mediator)
            => _mediator = mediator;
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(
    [FromQuery] string email,
    [FromQuery] string token)
        {
            var result = await _mediator.Send(
                new ConfirmEmailCommand(email, token));

            if (result.IsFailure)
                return StatusCode(result.Error.StatusCode!.Value, result.Error);

            return Ok(result.Value);
        }
    }
}
