using HotelManagement.Application.Interfaces.Services;
using HotelManagement.Domain.Common;
using HotelManagement.Domain.Common.Errors;
using HotelManagement.Domain.Enums;
using MediatR;

namespace HotelManagement.Application.Features.Bookings.Commands.CancelBooking
{
    public class CancelBookingCommandHandler : IRequestHandler<CancelBookingCommand, Result<string>>
    {
        private readonly IBookingService _bookingService;
        private readonly ICurrentUserService _currentUserService;
        public CancelBookingCommandHandler(IBookingService bookingService, ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
            _bookingService = bookingService;
        }

        public async Task<Result<string>> Handle(CancelBookingCommand request, CancellationToken cancellationToken)
        {
            // 1. Get GuestId from token
            var guestId = _currentUserService.UserId;
            if (string.IsNullOrEmpty(guestId))
                return Result.Failure<string>(Error.Unauthorized);
            //get boooking
            var booking = await _bookingService.GetBookingByIdAsync(
            request.BookingId, cancellationToken);

            if (booking == null)
                return Result.Failure<string>(Error.NotFound);
            // check Booking belong to Guest 
            if (booking.GuestId != guestId)
                return Result.Failure<string>(
                    new Error("Booking.Unauthorized",
                        "You can only cancel your own bookings.", 403));
            // check if Booking already cancelled
            if (booking.Status == BookingStatus.Cancelled)
                return Result.Failure<string>(
                    new Error("Booking.AlreadyCancelled",
                        "Booking is already cancelled.", 400));

            if (booking.Status == BookingStatus.CheckedIn)
                return Result.Failure<string>(
                    new Error("Booking.CannotCancel",
                        "Cannot cancel a booking that is already checked in.", 400));

            // Cancellation Policy —  24 before
            if (booking.CheckIn <= DateTime.UtcNow.AddHours(24))
                return Result.Failure<string>(
                    new Error("Booking.CancellationPolicy",
                        "Cannot cancel within 24 hours of check-in.", 400));
            //cancel booking
            await _bookingService.CancelBookingAsync(
          booking, cancellationToken);
            return Result.Success("Booking cancelled successfully.");
        }
    }
}
