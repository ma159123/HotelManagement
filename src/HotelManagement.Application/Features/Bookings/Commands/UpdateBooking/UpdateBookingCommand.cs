using HotelManagement.Domain.Common;
using HotelManagement.Domain.Enums;
using MediatR;

namespace HotelManagement.Application.Features.Bookings.Commands.UpdateBooking
{
    public record UpdateBookingCommand
    (
        int bookingId,
        BookingStatus BookingStatus
        ) : IRequest<Result<string>>;

}
