using HotelManagement.Application.Interfaces.Repositories;
using HotelManagement.Domain.Entities;
using HotelManagement.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Infrastructure.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public UserRepo(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task CreateGuestProfileAsync(GuestProfile guestProfile)
        {
            _context.GuestProfiles.Add(guestProfile);
            await SaveChanges();
        }

        public bool IsGuestExistAsync(string id)
        {
            var res = _context.Guests.FirstOrDefault(g => g.Id == id);
            if (res == null) return false;
            return true;
        }

        public bool IsGuestProfileExist(string idNumber)
        {
            var res = _context.GuestProfiles.FirstOrDefault(p => p.IdNumber == idNumber);
            if (res == null) return false;
            return true;
        }

        public GuestProfile? GetGuestProfile(int id)
        {
            var res = _context.GuestProfiles.FirstOrDefault(p => p.ProfileId == id);

            return res;
        }

        public async Task UpdateGuestAsync(Guest guest)
        {
            _context.Guests.Update(guest);
            await SaveChanges();
        }

        public void UpdateGuestProfile(GuestProfile guestProfile)
        {
            _context.GuestProfiles.Update(guestProfile);
        }

        public Guest? GetGuest(string id)
        {
            var res = _context.Guests.FirstOrDefault(g => g.Id == id);

            return res;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public Guest? GetGuestIncludeProfile(string id)
        {
            var res = _context.Guests.Include(u => u.Profile).FirstOrDefault(u => u.Id == id);

            return res;
        }
    }
}
