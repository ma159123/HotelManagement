using HotelManagement.Application.Interfaces.Repositories;
using HotelManagement.Domain.Entities;
using HotelManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Infrastructure.Repositories
{
    public class PaymentRepo(ApplicationDbContext _db) : IPaymentRepo
    {
        public async Task AddAsync(Payment payment, CancellationToken ct) => await _db.AddAsync(payment, ct);

        public IQueryable<Payment> GetAll() => _db.Payments;

        public async Task<Payment?> GetPaymentById(int BookingId)
        {
            return await _db.Payments.FirstOrDefaultAsync(p => p.BookingId == BookingId);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken) => await _db.SaveChangesAsync(cancellationToken);

    }
}
