namespace HotelManagement.Domain.Entities
{
    public class Amenity
    {
        public int AmenityId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        public ICollection<Room> Rooms { get; set; } = new List<Room>();

    }
}
