namespace HotelManagement.Domain.Entities
{
    public class RoomType
    {
        public int RoomTypeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int Capacity { get; set; }
        public decimal BasePrice { get; set; }
        public string? Amenities { get; set; }

        // Navigation
        public ICollection<Room> Rooms { get; set; } = new List<Room>();
    }
}
