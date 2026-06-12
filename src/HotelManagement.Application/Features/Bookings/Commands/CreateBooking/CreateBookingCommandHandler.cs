using HotelManagement.Application.Interfaces.Services;
using HotelManagement.Application.Mapping.Bookings;
using HotelManagement.Domain.Common;
using HotelManagement.Domain.Common.Errors;

namespace HotelManagement.Application.Features.Bookings.Commands.CreateBooking
{
    public class CreateBookingCommandHandler
    {
        private readonly IBookingService _bookingService;
        private readonly ICurrentUserService _currentUserService;
        public CreateBookingCommandHandler(IBookingService bookingService, ICurrentUserService currentUserService)
        {
            _bookingService = bookingService;
            _currentUserService = currentUserService;
        }
        public async Task<Result<int>> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            // 1. Get GuestId from token
            var guestId = _currentUserService.UserId;
            if (string.IsNullOrEmpty(guestId))
                return Result.Failure<int>(Error.Unauthorized);

            // 2. Check availability
            var isAvailable = await _bookingService.IsRoomAvailableAsync(request.RoomId, request.CheckIn, request.CheckOut, cancellationToken);
            if (!isAvailable)
                return Result.Failure<int>(new Error("Booking.RoomNotAvailable", "Room is not available for selected dates.", 400));

            //mapping
            var booking = request.ToBooking();
            // 3. Create booking
            var result = await _bookingService.CreateBookingAsync(booking, guestId, cancellationToken);

            return Result.Success(result.BookingId);
        }
    }
}
