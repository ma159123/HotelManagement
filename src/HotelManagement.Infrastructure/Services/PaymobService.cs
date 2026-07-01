using HotelManagement.Application.DTOs.Payments;
using HotelManagement.Application.Interfaces.Services;
using HotelManagement.Application.Settings;
using HotelManagement.Domain.Entities;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace HotelManagement.Infrastructure.Services
{
    // Infrastructure/Services/PaymobService.cs
    public class PaymobService : IPaymobService
    {
        private readonly HttpClient _httpClient;
        private readonly PaymobSettings _settings;

        public PaymobService(HttpClient httpClient, IOptions<PaymobSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
        }

        // ─── Step 1: Auth Token ──────────────────────
        public async Task<string> GetAuthTokenAsync(CancellationToken ct = default)
        {
            var response = await _httpClient.PostAsJsonAsync(
                $"{_settings.BaseUrl}/auth/tokens",
                new { api_key = _settings.ApiKey },
                ct);

            response.EnsureSuccessStatusCode();

            var result = await response.Content
                .ReadFromJsonAsync<JsonElement>(cancellationToken: ct);

            return result.GetProperty("token").GetString()!;
        }

        // ─── Step 2: Register Order ──────────────────
        public async Task<string> RegisterOrderAsync(
            string authToken,
            int bookingId,
            decimal amount,
            CancellationToken ct = default)
        {
            var body = new
            {
                auth_token = authToken,
                delivery_needed = false,
                amount_cents = (int)(amount * 100), // Paymob بيشتغل بـ Cents
                currency = "EGP",
                merchant_order_id = $"{bookingId}-{DateTime.UtcNow.Ticks}", // BookingId بتاعك
                items = Array.Empty<object>()
            };

            var response = await _httpClient.PostAsJsonAsync(
                $"{_settings.BaseUrl}/ecommerce/orders", body, ct);
            var errorBody = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();

            var result = await response.Content
                .ReadFromJsonAsync<JsonElement>(cancellationToken: ct);

            return result.GetProperty("id").GetInt64().ToString()!;
        }

        // ─── Step 3: Payment Key ─────────────────────
        public async Task<string> GetPaymentKeyAsync(
            string authToken,
            string paymobOrderId,
            decimal amount,
            Booking booking,
            CancellationToken ct = default)
        {
            var body = new
            {
                auth_token = authToken,
                amount_cents = (int)(amount * 100),
                expiration = 3600, // ساعة
                order_id = paymobOrderId,
                billing_data = new
                {
                    first_name = booking.Guest.FullName,
                    last_name = ".",
                    email = booking.Guest.Email,
                    phone_number = booking.Guest.PhoneNumber ?? "N/A",
                    apartment = "N/A",
                    floor = "N/A",
                    street = "N/A",
                    building = "N/A",
                    shipping_method = "N/A",
                    postal_code = "N/A",
                    city = "N/A",
                    country = "EG",
                    state = "N/A"
                },
                currency = "EGP",
                integration_id = _settings.IntegrationId
            };

            var response = await _httpClient.PostAsJsonAsync(
                $"{_settings.BaseUrl}/acceptance/payment_keys", body, ct);

            response.EnsureSuccessStatusCode();

            var result = await response.Content
                .ReadFromJsonAsync<JsonElement>(cancellationToken: ct);

            return result.GetProperty("token").GetString()!;
        }

        // ─── Verify Webhook HMAC ─────────────────────
        public bool VerifyWebhookHmac(string receivedHmac, PaymobTransactionDto transaction)
        {
            // Paymob بيبعت HMAC عشان تتأكد إن الـ Request جاي منه فعلاً
            var data = string.Concat(
                transaction.Order.Id,
                transaction.Success.ToString().ToLower(),
                transaction.AmountCents,
                transaction.Id
            );

            using var hmac = new HMACSHA512(
                Encoding.UTF8.GetBytes(_settings.HmacSecret));

            var computedHmac = Convert.ToHexString(
                hmac.ComputeHash(Encoding.UTF8.GetBytes(data))).ToLower();

            return computedHmac == receivedHmac.ToLower();
        }
    }
}
