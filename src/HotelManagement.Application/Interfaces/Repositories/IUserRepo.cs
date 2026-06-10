using HotelManagement.Domain.Entities;

namespace HotelManagement.Application.Interfaces.Repositories
{
    public interface IUserRepo
    {
        Task CreateGuestProfileAsync(GuestProfile guestProfile);
        bool IsGuestProfileExist(string idNumber);
        GuestProfile? GetGuestProfile(int id);
        bool IsGuestExistAsync(string id);
        void UpdateGuestProfile(GuestProfile guestProfile);
        Guest? GetGuest(string id);
        Guest? GetGuestIncludeProfile(string id);
        Task UpdateGuestAsync(Guest guest);
        Task SaveChanges();
    }
}
