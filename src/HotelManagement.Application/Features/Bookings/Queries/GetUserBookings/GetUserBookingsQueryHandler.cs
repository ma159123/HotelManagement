using HotelManagement.Application.DTOs.Booking;
using HotelManagement.Application.Interfaces.Services;
using HotelManagement.Domain.Common;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Application.Features.Bookings.Queries.GetUserBookings
{
    public class GetUserBookingsQueryHandler : IRequestHandler<GetUserBookingsQuery, Result<PagedUserBookingsResponse>>
    {
        private readonly IBookingService _bookingService;
        private readonly ICurrentUserService _currentUserService;
        public GetUserBookingsQueryHandler(IBookingService bookingService, ICurrentUserService currentUserService)
        {
            _bookingService = bookingService;
            _currentUserService = currentUserService;
        }
        public async Task<Result<PagedUserBookingsResponse>> Handle(GetUserBookingsQuery request, CancellationToken cancellationToken)
        {
            //get current user
            var userId = _currentUserService.UserId;
            //get user bookings
            var bookings = _bookingService.GetBookings().Where(b => b.GuestId == userId);
            //filtering
            if (request.Status != null)
                bookings = bookings.Where((b) => b.Status == request.Status);


            //pagination
            int totalCount = await bookings.CountAsync(cancellationToken);
            var totalPages = (int)Math.Ceiling(totalCount / (double)request.pageSize);
            int skip = (request.page - 1) * request.pageSize;

            var bookingList = await bookings.Skip(skip)
                                    .Take(request.pageSize)
                                    .ToListAsync(cancellationToken);
            //map list 
            var result = bookingList.Adapt<List<GetUserBookingResult>>();

            var pagedResponse = new PagedUserBookingsResponse
            {
                Data = result,
                PageSize = request.pageSize,
                Page = request.page,
                TotalCount = totalCount,
                TotalPages = totalPages,
            };

            // return
            return Result.Success(pagedResponse);
        }
    }
}