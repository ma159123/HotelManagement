using HotelManagement.Application.Features.Rooms.Commands.Create;
using HotelManagement.Application.Features.Rooms.Commands.Update;
using HotelManagement.Application.Features.Rooms.Queries.GetAllRooms;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly ISender _mediator;

        public RoomsController(ISender mediator)
        {
            _mediator = mediator;
        }

        //rooms

        [HttpGet]
        public async Task<IActionResult> GetAllRooms([FromQuery] GetAllRoomsQuery getAllRoomsQuery)
        {
            var result = await _mediator.Send(getAllRoomsQuery);
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> CreateRoom(CreateRoomCommand roomCommand)
        {
            var result = await _mediator.Send(roomCommand);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRoom([FromForm] UpdateRoomCommand roomCommand)
        {
            var result = await _mediator.Send(roomCommand);
            return Ok(result);
        }

    }
}
