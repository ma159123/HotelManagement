using HotelManagement.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace HotelManagement.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public UserStatus Status { get; set; } = UserStatus.Active;
    }
}
