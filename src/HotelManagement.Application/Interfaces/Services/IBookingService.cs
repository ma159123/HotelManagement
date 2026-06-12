using HotelManagement.Domain.Entities;

namespace HotelManagement.Application.Interfaces.Services
{
    public interface IBookingService
    {
        Task<bool> IsRoomAvailableAsync(int roomId, DateTime checkIn, DateTime checkOut, CancellationToken ct = default);
        Task<decimal> CalculateTotalPriceAsync(int roomId, DateTime checkIn, DateTime checkOut, List<int>? serviceIds, CancellationToken ct = default);
        Task<Booking> CreateBookingAsync(Booking command, string guestId, CancellationToken ct = default);
    }
}
