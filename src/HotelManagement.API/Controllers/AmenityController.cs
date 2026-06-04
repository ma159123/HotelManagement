using HotelManagement.Application.Features.Aminity.Commands.Create;
using HotelManagement.Application.Features.Aminity.Commands.Delete;
using HotelManagement.Application.Features.Aminity.Commands.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmenityController : ControllerBase
    {
        private readonly ISender _mediator;

        public AmenityController(ISender mediator)
        {
            _mediator = mediator;
        }

        //rooms

        //[HttpGet]
        //public async Task<IActionResult> GetAllAmenities()
        //{
        //    var query = new GetA();
        //    var result = await _mediator.Send(query);
        //    return Ok(result);
        //}


        [HttpPost("CreateAmenity")]
        public async Task<IActionResult> CreateAmenity(CreateAminityCommand amenityCommand)
        {
            var result = await _mediator.Send(amenityCommand);
            return Ok(result);
        }

        [HttpPut("UpdateAmenity")]
        public async Task<IActionResult> UpdateAmenity([FromForm] UpdateAminityCommand amenityCommand)
        {
            var result = await _mediator.Send(amenityCommand);
            return Ok(result);
        }


        [HttpDelete("DeleteAmenity")]
        public async Task<IActionResult> DeleteAmenity([FromForm] DeleteAminityCommand amenityCommand)
        {
            var result = await _mediator.Send(amenityCommand);
            return Ok(result);
        }
    }
}
