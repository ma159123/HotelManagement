using HotelManagement.Application.DTOs.Payments;
using HotelManagement.Domain.Entities;

namespace HotelManagement.Application.Interfaces.Services
{
    public interface IPaymobService
    {
        Task<string> GetAuthTokenAsync(CancellationToken ct = default);

        Task<string> RegisterOrderAsync(
            string authToken,
            int bookingId,
            decimal amount,
            CancellationToken ct = default);

        Task<string> GetPaymentKeyAsync(
            string authToken,
            string paymobOrderId,
            decimal amount,
            Booking booking,
            CancellationToken ct = default);

        bool VerifyWebhookHmac(string hmac, PaymobTransactionDto transaction);
    }
}
