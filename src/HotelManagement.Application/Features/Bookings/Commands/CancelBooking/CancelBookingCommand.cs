using HotelManagement.Domain.Common;
using MediatR;

namespace HotelManagement.Application.Features.Bookings.Commands.CancelBooking
{
    public record CancelBookingCommand(int BookingId)
     : IRequest<Result<string>>;
}
