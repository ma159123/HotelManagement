using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Domain.Entities
{
    public class BookingService
    {
        public int BookingServiceId { get; set; }
        public int BookingId { get; set; }
        public int ServiceId { get; set; }
        public int Quantity { get; set; } = 1;
        public decimal Price { get; set; }
        [ForeignKey("BookingId")]
        public Booking Booking { get; set; } = null!;
        [ForeignKey("ServiceId")]
        public Service Service { get; set; } = null!;
    }
}
