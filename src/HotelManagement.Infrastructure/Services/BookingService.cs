using HotelManagement.Application.Interfaces.Services;
using HotelManagement.Domain.Entities;
using HotelManagement.Domain.Enums;
using HotelManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Infrastructure.Services
{
    public class BookingService : IBookingService
    {
        private readonly ApplicationDbContext _context;
        public BookingService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task<decimal> CalculateTotalPriceAsync(int roomId, DateTime checkIn, DateTime checkOut, List<int>? serviceIds, CancellationToken ct = default)
        {
            decimal totalPrice = 0;
            //get room price
            var room = _context.Rooms.FirstOrDefault(r => r.RoomId == roomId);
        }

        public async Task<Booking> CreateBookingAsync(Booking booking, string guestId, CancellationToken ct = default)
        {
            var result = await _context.Bookings.AddAsync(booking, ct);

            return result.Entity;
        }

        public async Task<bool> IsRoomAvailableAsync(int roomId, DateTime checkIn, DateTime checkOut, CancellationToken ct = default)
        {
            var hasOverlap = await _context.Bookings.AnyAsync(b => b.RoomId == roomId &&
             b.Status != BookingStatus.Cancelled && b.CheckIn < checkOut && b.CheckOut > checkIn, ct);
            return !hasOverlap;
        }
    }
}
