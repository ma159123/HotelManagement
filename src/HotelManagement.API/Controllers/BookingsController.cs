using HotelManagement.Application.Features.Bookings.Commands.CancelBooking;
using HotelManagement.Application.Features.Bookings.Commands.CreateBooking;
using HotelManagement.Application.Features.Bookings.Queries.GetUserBookings;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BookingsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserBookings([FromQuery] GetUserBookingsQuery query)
        {
            var res = await _mediator.Send(query);
            return Ok(res);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateUserBooking(CreateBookingCommand command)
        {
            var res = await _mediator.Send(command);
            return Ok(res);
        }

        [HttpPut("cancel")]
        [Authorize]
        public async Task<IActionResult> CancelUserBooking(CancelBookingCommand command)
        {
            var res = await _mediator.Send(command);
            return Ok(res);
        }
    }
}
