using HotelManagement.Application.Features.RoomTypes.Commands.Create;
using HotelManagement.Application.Features.RoomTypes.Commands.Delete;
using HotelManagement.Application.Features.RoomTypes.Commands.Update;
using HotelManagement.Application.Features.RoomTypes.Queries.GetAllRoomTypesQuery;
using HotelManagement.Application.Features.RoomTypes.Queries.GetRoomTypeById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        [AllowAnonymous]
        public async Task<IActionResult> GetAllRoomTypes(int Page, int PageSize)
        {
            var result = await _mediator.Send(new GetAllRoomTypesQuery(Page, PageSize));
            return Ok(result);
        }

        [HttpGet("{Id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRoomTypeById(int Id)
        {
            var result = await _mediator.Send(new GetRoomTypeByIdQuery(Id));
            return Ok(result);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateRoomType(CreateRoomTypeCommand roomTypeCommand)
        {
            var result = await _mediator.Send(roomTypeCommand);
            return Ok(result);
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRoomType([FromForm] UpdateRoomTypeCommand roomTypeCommand)
        {
            var result = await _mediator.Send(roomTypeCommand);
            return Ok(result);
        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRoomType([FromForm] DeleteRoomTypeCommand roomTypeCommand)
        {
            var result = await _mediator.Send(roomTypeCommand);
            return Ok(result);
        }
    }
}
