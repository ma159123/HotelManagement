using HotelManagement.Application.Features.Rooms.Commands.Create;
using HotelManagement.Application.Features.Rooms.Commands.Delete;
using HotelManagement.Application.Features.Rooms.Commands.Update;
using HotelManagement.Application.Features.Rooms.Queries.GetAllRooms;
using HotelManagement.Application.Features.Rooms.Queries.GetAllRoomTypesQuery;
using HotelManagement.Application.Features.Rooms.Queries.GetRoomTypeById;
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
        public async Task<IActionResult> GetAllRooms()
        {
            var query = new GetAllRoomsQuery();
            var result = await _mediator.Send(query);
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
        //-------------room types

        [HttpGet("GetAllRoomTypes")]
        public async Task<IActionResult> GetAllRoomTypes(int Page, int PageSize)
        {
            var result = await _mediator.Send(new GetAllRoomTypesQuery(Page, PageSize));
            return Ok(result);
        }

        [HttpGet("GetRoomTypeById")]
        public async Task<IActionResult> GetRoomTypeById(int Id)
        {
            var result = await _mediator.Send(new GetRoomTypeByIdQuery(Id));
            return Ok(result);
        }

        [HttpPost("CreateRoomType")]
        public async Task<IActionResult> CreateRoomType(CreateRoomTypeCommand roomTypeCommand)
        {
            var result = await _mediator.Send(roomTypeCommand);
            return Ok(result);
        }
        [HttpPut("UpdateRoomType")]
        public async Task<IActionResult> UpdateRoomType([FromForm] UpdateRoomTypeCommand roomTypeCommand)
        {
            var result = await _mediator.Send(roomTypeCommand);
            return Ok(result);
        }
        [HttpDelete("DeleteRoomType")]
        public async Task<IActionResult> DeleteRoomType([FromForm] DeleteRoomTypeCommand roomTypeCommand)
        {
            var result = await _mediator.Send(roomTypeCommand);
            return Ok(result);
        }
    }
}
