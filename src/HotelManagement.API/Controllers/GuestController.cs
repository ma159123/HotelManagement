using HotelManagement.Application.Features.GuestFeature.Commands.Update;
using HotelManagement.Application.Features.GuestFeature.GuestProfile.Create;
using HotelManagement.Application.Features.GuestFeature.GuestProfile.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestController : ControllerBase
    {
        private readonly ISender _mediator;

        public GuestController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("guest-profile")]
        public async Task<IActionResult> CreateGuestProfile(CreateGuestProfileCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPatch("profile/id-info")]
        public async Task<IActionResult> UpdateGuestProfile(UpdateGuestProfileCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPatch("profile/basic-info")]
        public async Task<IActionResult> UpdateGuest(UpdateGuestCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
