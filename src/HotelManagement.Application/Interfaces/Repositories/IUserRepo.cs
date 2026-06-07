using HotelManagement.Domain.Entities;

namespace HotelManagement.Application.Interfaces.Repositories
{
    public interface IUserRepo
    {
        void CreateGuestProfile(GuestProfile guestProfile);
        bool IsGuestProfileExist(string idNumber);
        GuestProfile? GetGuestProfile(int id);
        Task<bool> IsGuestExistAsync(string id);
        void UpdateGuestProfile(GuestProfile guestProfile);
        Task<Guest?> GetGuest(string id);
        Task UpdateGuestAsync(Guest guest);
        Task SaveChanges();
    }
}
