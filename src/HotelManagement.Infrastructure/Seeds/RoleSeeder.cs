using HotelManagement.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace HotelManagement.Infrastructure.Seeds
{
    public static class RoleSeeder
    {
        public static async Task SeedRolesAsync(RoleManager<Role> roleManager)
        {
            string[] roles = ["Admin", "Guest", "Staff"];

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new Role { Name = role });
            }
        }
    }
}
