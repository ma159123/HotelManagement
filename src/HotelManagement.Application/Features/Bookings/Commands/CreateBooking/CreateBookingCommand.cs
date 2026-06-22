using HotelManagement.Domain.Common;
using MediatR;

namespace HotelManagement.Application.Features.Bookings.Commands.CreateBooking;

public record CreateBookingCommand
(
int RoomId,
DateTime CheckIn,
DateTime CheckOut,
List<int>? ServiceIds
) : IRequest<Result<int?>>;
