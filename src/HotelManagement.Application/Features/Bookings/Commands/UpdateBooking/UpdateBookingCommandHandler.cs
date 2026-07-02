using HotelManagement.Application.Interfaces.Services;
using HotelManagement.Domain.Common;
using HotelManagement.Domain.Common.Errors;
using MediatR;

namespace HotelManagement.Application.Features.Bookings.Commands.UpdateBooking
{
    public class UpdateBookingCommandHandler : IRequestHandler<UpdateBookingCommand, Result<string>>
    {
        private readonly IBookingService _bookingService;
        private readonly ICurrentUserService _currentUserService;
        public UpdateBookingCommandHandler(IBookingService bookingService, ICurrentUserService currentUserService)
        {
            _bookingService = bookingService;
            _currentUserService = currentUserService;
        }
        public async Task<Result<string>> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
        {
            //  Get GuestId from token
            var userId = _currentUserService.UserId;
            if (string.IsNullOrEmpty(userId))
                return Result.Failure<string?>(Error.Unauthorized);
            // get booking
            var booking = await _bookingService.GetBookingByIdAsync(request.bookingId, cancellationToken);
            if (booking == null)
            {
                return Result.Failure<string?>(Error.NotFound);
            }
            booking.Status = request.BookingStatus;
            booking.PaymentId = request.paymentId;
            await _bookingService.ConfirmBookingAsync(booking, cancellationToken);
            return Result.Success<string>("Booking Confirmed");
        }
    }
}
