using HotelManagement.Domain.Enums;

namespace HotelManagement.Domain.Entities
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int BookingId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } = "Card";
        public string? TransactionId { get; set; }    // Paymob Transaction ID
        public string? PaymobOrderId { get; set; }    // Paymob Order ID
        public string? PaymentKey { get; set; }        // Paymob Payment Key
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
        public DateTime? PaymentDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public Booking Booking { get; set; } = null!;
    }
}
