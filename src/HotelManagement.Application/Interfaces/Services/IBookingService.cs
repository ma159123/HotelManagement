using HotelManagement.Domain.Entities;

namespace HotelManagement.Application.Interfaces.Services
{
    public interface IBookingService
    {
        Task<bool> IsRoomAvailableAsync(int roomId, DateTime checkIn, DateTime checkOut, CancellationToken ct = default);
        decimal CalculateTotalPrice(int roomId, DateTime checkIn, DateTime checkOut, List<Service>? services, CancellationToken ct = default);
        Task<int?> CreateBookingAsync(Booking booking, List<Service> services, CancellationToken ct = default);
        IQueryable<Booking> GetBookings();
        Task<List<Service>> GetServicesByIdsAsync(List<int> ids, CancellationToken ct);
        Task<Booking?> GetBookingByIdAsync(int bookingId, CancellationToken ct = default);
        Task CancelBookingAsync(Booking booking, CancellationToken ct = default);
        Task ConfirmBookingAsync(Booking booking, CancellationToken ct = default);
        Task CheckInAsync(Booking booking, CancellationToken ct = default);
        Task CheckOutAsync(Booking booking, CancellationToken ct = default);
    }
}
