namespace HotelManagement.Domain.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string GuestId { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsRevoked { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public Guest Guest { get; set; }
    }
}
