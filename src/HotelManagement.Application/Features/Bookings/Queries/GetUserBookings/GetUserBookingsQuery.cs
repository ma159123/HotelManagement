using HotelManagement.Application.DTOs.Booking;
using HotelManagement.Domain.Common;
using HotelManagement.Domain.Enums;
using MediatR;

namespace HotelManagement.Application.Features.Bookings.Queries.GetUserBookings
{
    public record GetUserBookingsQuery
    (
        int page = 1,
        int pageSize = 10,
        BookingStatus? Status = null
        ) : IRequest<Result<PagedUserBookingsResponse>>;
}
