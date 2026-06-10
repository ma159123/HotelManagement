using HotelManagement.Domain.Entities;
using HotelManagement.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace HotelManagement.Infrastructure.Seeds
{
    public static class AdminSeeder
    {
        public static async Task SeedAdminAsync(UserManager<ApplicationUser> userManager)
        {
            var adminEmail = "admin@hotel.com";

            var existingAdmin = await userManager.FindByEmailAsync(adminEmail);
            if (existingAdmin != null) return;

            var admin = new ApplicationUser
            {
                FullName = "Hotel Admin",
                Email = adminEmail,
                UserName = adminEmail,
                EmailConfirmed = true,
                Status = UserStatus.Active,
                CreatedAt = DateTime.UtcNow
            };

            var result = await userManager.CreateAsync(admin, "Admin@123456");

            if (result.Succeeded)
                await userManager.AddToRoleAsync(admin, "Admin");
        }
    }
}
