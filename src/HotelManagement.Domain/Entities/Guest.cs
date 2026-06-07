using HotelManagement.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Domain.Entities
{
    public class Guest : IdentityUser
    {
        public string FullName { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public int ProfileId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public GuestStatus Status { get; set; } = GuestStatus.Active;
        [ForeignKey(nameof(ProfileId))]
        virtual public GuestProfile Profile { get; set; }
    }
}
