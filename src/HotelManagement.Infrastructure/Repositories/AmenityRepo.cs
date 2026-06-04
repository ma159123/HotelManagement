using HotelManagement.Application.Interfaces.Repositories;
using HotelManagement.Domain.Entities;
using HotelManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Infrastructure.Repositories
{
    public class AmenityRepo : IAmenityRepo
    {
        private readonly ApplicationDbContext _context;
        public AmenityRepo(ApplicationDbContext context)
        {
            this._context = context;
        }
        public void CreateAmenity(Amenity amenity)
        {
            _context.Amenities.Add(amenity);
        }

        public void DeleteAmenity(Amenity amenity)
        {
            _context.Amenities.Remove(amenity);
        }

        public Task<Amenity?> GetAmenityById(int id)
        {
            return _context.Amenities.FirstOrDefaultAsync((a) => a.AmenityId == id);
        }

        public IQueryable<Amenity> GetAminities()
        {
            return _context.Amenities.AsNoTracking();
        }

        public async Task<bool> IsAmenityExist(string name)
        {
            var amenity = await _context.Amenities.FirstOrDefaultAsync((a) => a.Name.Equals(name));
            if (amenity == null) return false;
            return true;
        }



        public void UpdateAmenity(Amenity amenity)
        {
            _context.Amenities.Update(amenity);
        }
        public async Task<int> saveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
