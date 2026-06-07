using HotelManagement.Application.Interfaces.Repositories;
using HotelManagement.Domain.Entities;
using HotelManagement.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace HotelManagement.Infrastructure.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly UserManager<Guest> _userManager;
        private readonly ApplicationDbContext _context;
        public UserRepo(ApplicationDbContext context, UserManager<Guest> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public void CreateGuestProfile(GuestProfile guestProfile)
        {
            _context.GuestProfiles.Add(guestProfile);
        }

        public async Task<bool> IsGuestExistAsync(string id)
        {
            var res = await _userManager.FindByIdAsync(id);
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
            await _userManager.UpdateAsync(guest);
        }

        public void UpdateGuestProfile(GuestProfile guestProfile)
        {
            _context.GuestProfiles.Update(guestProfile);
        }

        public async Task<Guest?> GetGuest(string id)
        {
            var res = await _userManager.FindByIdAsync(id);

            return res;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
