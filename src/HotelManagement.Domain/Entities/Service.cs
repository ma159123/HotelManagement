using HotelManagement.Domain.Enums;

namespace HotelManagement.Domain.Entities
{
    public class Service
    {
        public int ServiceId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public ServiceStatus Status { get; set; } = ServiceStatus.Active;
        public ICollection<BookingService> BookingServices { get; set; } = new List<BookingService>();
    }
}
