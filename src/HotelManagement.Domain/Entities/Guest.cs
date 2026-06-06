using HotelManagement.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace HotelManagement.Domain.Entities
{
    public class Guest : IdentityUser
    {
        public string FullName { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public GuestStatus Status { get; set; } = GuestStatus.Active;
    }
}
