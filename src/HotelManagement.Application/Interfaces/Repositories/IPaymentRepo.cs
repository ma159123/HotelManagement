using HotelManagement.Domain.Entities;

namespace HotelManagement.Application.Interfaces.Repositories
{
    public interface IPaymentRepo
    {
        Task AddAsync(Payment payment, CancellationToken ct);
        IQueryable<Payment> GetAll();
        Task<Payment?> GetPaymentById(int BookingId);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
