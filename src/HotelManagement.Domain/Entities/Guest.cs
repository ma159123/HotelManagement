namespace HotelManagement.Domain.Entities
{
    public class Guest : ApplicationUser
    {
        public string? Phone { get; set; }
        public string? Address { get; set; }
        virtual public GuestProfile? Profile { get; set; }
    }
}
