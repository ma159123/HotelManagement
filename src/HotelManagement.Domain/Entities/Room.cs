using HotelManagement.Domain.Enums;

namespace HotelManagement.Domain.Entities
{
    public class Room
    {
        public int RoomId { get; set; }
        public int RoomTypeId { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public int Floor { get; set; }
        public string ViewType { get; set; } = string.Empty;
        public RoomStatus Status { get; set; }
        public decimal PricePerNight { get; set; }
        public string? ImageUrl { get; set; }

        // Navigation Properties
        public RoomType RoomType { get; set; } = null!;
    }
}
