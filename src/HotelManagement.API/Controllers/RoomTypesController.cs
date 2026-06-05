using HotelManagement.Application.Features.Rooms.Commands.Create;
using HotelManagement.Application.Features.Rooms.Commands.Delete;
using HotelManagement.Application.Features.Rooms.Commands.Update;
using HotelManagement.Application.Features.Rooms.Queries.GetAllRoomTypesQuery;
using HotelManagement.Application.Features.Rooms.Queries.GetRoomTypeById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomTypesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RoomTypesController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRoomTypes(int Page, int PageSize)
        {
            var result = await _mediator.Send(new GetAllRoomTypesQuery(Page, PageSize));
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetRoomTypeById(int Id)
        {
            var result = await _mediator.Send(new GetRoomTypeByIdQuery(Id));
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateRoomType(CreateRoomTypeCommand roomTypeCommand)
        {
            var result = await _mediator.Send(roomTypeCommand);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateRoomType([FromForm] UpdateRoomTypeCommand roomTypeCommand)
        {
            var result = await _mediator.Send(roomTypeCommand);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteRoomType([FromForm] DeleteRoomTypeCommand roomTypeCommand)
        {
            var result = await _mediator.Send(roomTypeCommand);
            return Ok(result);
        }
    }
}
