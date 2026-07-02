using HotelManagement.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Domain.Entities
{
    public class Booking
    {
        public int BookingId { get; set; }
        public string GuestId { get; set; } = string.Empty;
        public int RoomId { get; set; }
        public int? PaymentId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public BookingStatus Status { get; set; } = BookingStatus.Pending;
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("GuestId")]
        public Guest Guest { get; set; } = null!;
        [ForeignKey("RoomId")]
        public Room Room { get; set; } = null!;
        [ForeignKey("PaymentId")]
        public Payment? Payment { get; set; } = null!;
        public ICollection<BookingService> BookingServices { get; set; } = new List<BookingService>();
    }
}
