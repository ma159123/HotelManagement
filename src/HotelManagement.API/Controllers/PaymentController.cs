using HotelManagement.Application.Features.PaymentFeature.Commands.PayOrder;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ISender _mediator;

        public PaymentController(ISender mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("Pay")]
        public async Task<IActionResult> PayOrder(InitiatePaymentCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

    }
}
