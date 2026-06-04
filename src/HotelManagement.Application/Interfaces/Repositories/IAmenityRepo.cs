using HotelManagement.Domain.Entities;

namespace HotelManagement.Application.Interfaces.Repositories
{
    public interface IAmenityRepo
    {
        public IQueryable<Amenity> GetAminities();
        public void UpdateAmenity(Amenity amenity);
        public void DeleteAmenity(Amenity amenity);
        public void CreateAmenity(Amenity amenity);
        public Task<Amenity?> GetAmenityById(int id);
        public Task<bool> IsAmenityExist(string name);

        public Task<int> saveChangesAsync();
    }
}
