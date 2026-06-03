using HotelManagement.Application.Features.Rooms.Commands.Room.Create;
using HotelManagement.Application.Features.Rooms.Commands.Room.Update;
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllRoomsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("CreateRoomType")]
        public async Task<IActionResult> CreateRoomType(CreateRoomTypeCommand roomTypeCommand)
        {
            var result = await _mediator.Send(roomTypeCommand);
            return Ok(result);
        }
        [HttpPost("CreateRoom")]
        public async Task<IActionResult> CreateRoom(CreateRoomCommand roomCommand)
        {
            var result = await _mediator.Send(roomCommand);
            return Ok(result);
        }

        [HttpPut("UpdateRoom")]
        public async Task<IActionResult> UpdateRoom([FromForm] UpdateRoomCommand roomCommand)
        {
            var result = await _mediator.Send(roomCommand);
            return Ok(result);
        }
        [HttpPut("UpdateRoomType")]
        public async Task<IActionResult> UpdateRoomType([FromForm] UpdateRoomTypeCommand roomTypeCommand)
        {
            var result = await _mediator.Send(roomTypeCommand);
            return Ok(result);
        }
    }
}
