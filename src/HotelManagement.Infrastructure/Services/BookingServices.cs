using HotelManagement.Application.Interfaces.Services;
using HotelManagement.Domain.Entities;
using HotelManagement.Domain.Enums;
using HotelManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Infrastructure.Services
{
    public class BookingServices : IBookingService
    {
        private readonly ApplicationDbContext _context;
        public BookingServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public decimal CalculateTotalPrice(int roomId, DateTime checkIn, DateTime checkOut, List<Service>? services, CancellationToken ct = default)
        {
            decimal totalPrice = 0;
            //get room price
            var room = _context.Rooms.FirstOrDefault(r => r.RoomId == roomId);
            int bookingDays = (checkOut - checkIn).Days;
            totalPrice = room.PricePerNight * bookingDays;
            foreach (var service in services)
            {
                totalPrice += service.Price;
            }
            return totalPrice;
        }

        public async Task CancelBookingAsync(Booking booking, CancellationToken cancellationToken)
        {
            booking.Status = BookingStatus.Cancelled;
            await _saveChangesAsync(cancellationToken);
        }

        public async Task CheckInAsync(Booking booking, CancellationToken ct = default)
        {
            booking.Status = BookingStatus.CheckedIn;
            await _saveChangesAsync(ct);
        }

        public async Task CheckOutAsync(Booking booking, CancellationToken ct = default)
        {
            booking.Status = BookingStatus.CheckedOut;
            await _saveChangesAsync(ct);
        }

        public async Task ConfirmBookingAsync(Booking booking, CancellationToken ct = default)
        {
            booking.Status = BookingStatus.Confirmed;
            await _saveChangesAsync(ct);
        }

        public async Task<int?> CreateBookingAsync(Booking booking, List<Service> services, CancellationToken ct = default)
        {
            //Transaction
            await using var transaction = await _context.Database
                .BeginTransactionAsync(ct);

            try
            {

                await _context.Bookings.AddAsync(booking, ct);
                await _context.SaveChangesAsync(ct);

                //  make BookingServices
                if (services.Any())
                {
                    var bookingServices = services.Select(s => new BookingService
                    {
                        BookingId = booking.BookingId,
                        ServiceId = s.ServiceId,
                        Quantity = 1,
                        Price = s.Price
                    }).ToList();

                    await _context.BookingServices.AddRangeAsync(
                        bookingServices, ct);

                    await _context.SaveChangesAsync(ct);
                }

                // Commit
                await transaction.CommitAsync(ct);

                return booking.BookingId;
            }
            catch (Exception)
            {
                // 10 - لو أي حاجة فشلت — Rollback
                await transaction.RollbackAsync(ct);

                return null;
            }
        }

        public Task<Booking?> GetBookingByIdAsync(int id, CancellationToken cancellationToken)
        {
            return _context.Bookings.FirstOrDefaultAsync(b => b.BookingId == id, cancellationToken);
        }

        public IQueryable<Booking> GetBookings()
        {
            var booking = _context.Bookings;
            return booking.AsNoTracking();
        }

        public async Task<List<Service>> GetServicesByIdsAsync(List<int> ids, CancellationToken ct = default)
        {
            var services = await _context.Services
                    .Where(s => ids.Contains(s.ServiceId) &&
                                s.Status == ServiceStatus.Active)
                    .ToListAsync(ct);
            return services;
        }

        public async Task<bool> IsRoomAvailableAsync(int roomId, DateTime checkIn, DateTime checkOut, CancellationToken ct = default)
        {
            var hasOverlap = await _context.Bookings.AnyAsync(b => b.RoomId == roomId &&
             b.Status != BookingStatus.Cancelled && b.CheckIn < checkOut && b.CheckOut > checkIn, ct);
            return !hasOverlap;
        }


        async Task _saveChangesAsync(CancellationToken ct)
        {
            await _context.SaveChangesAsync(ct);
        }
    }
}
