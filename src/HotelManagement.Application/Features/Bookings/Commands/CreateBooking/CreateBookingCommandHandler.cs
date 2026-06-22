using HotelManagement.Application.Interfaces.Services;
using HotelManagement.Application.Mapping.Bookings;
using HotelManagement.Domain.Common;
using HotelManagement.Domain.Common.Errors;
using HotelManagement.Domain.Entities;
using MediatR;

namespace HotelManagement.Application.Features.Bookings.Commands.CreateBooking
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, Result<int?>>
    {
        private readonly IBookingService _bookingService;
        private readonly ICurrentUserService _currentUserService;
        public CreateBookingCommandHandler(IBookingService bookingService, ICurrentUserService currentUserService)
        {
            _bookingService = bookingService;
            _currentUserService = currentUserService;
        }
        public async Task<Result<int?>> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            // 1. Get GuestId from token
            var guestId = _currentUserService.UserId;
            if (string.IsNullOrEmpty(guestId))
                return Result.Failure<int?>(Error.Unauthorized);

            // 2. Check availability
            var isAvailable = await _bookingService.IsRoomAvailableAsync(request.RoomId, request.CheckIn, request.CheckOut, cancellationToken);
            if (!isAvailable)
                return Result.Failure<int?>(new Error("Booking.RoomNotAvailable", "Room is not available for selected dates.", 400));
            //mapping
            var booking = request.ToBooking();
            booking.GuestId = guestId;
            //get services
            List<Service> services = new();
            if (request.ServiceIds != null && request.ServiceIds.Any())
            {
                services = await _bookingService.GetServicesByIdsAsync(request.ServiceIds, cancellationToken);

                //check ServiceIds exist
                if (services.Count != request.ServiceIds.Count)
                    return Result.Failure<int?>(
                        new Error("Booking.InvalidService", "One or more services not found.", 400));
            }

            //get total price
            decimal totalPrice = _bookingService.CalculateTotalPrice(request.RoomId, request.CheckIn, request.CheckOut, services, cancellationToken);

            booking.TotalPrice = totalPrice;
            // 3. Create booking
            var result = await _bookingService.CreateBookingAsync(booking, services, cancellationToken);
            if (result == null)
            {
                return Result.Failure<int?>(
                      new Error("Booking.Failed", "Failed to create booking.", 500));
            }
            return Result.Success(result);
        }
    }
}
