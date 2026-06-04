using HotelManagement.Application.Interfaces.Repositories;
using HotelManagement.Domain.Entities;
using HotelManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Infrastructure.Repositories
{
    public class RoomsRepo : IRoomsRepo
    {
        private readonly ApplicationDbContext _context;
        public RoomsRepo(ApplicationDbContext context)
        {
            this._context = context;
        }
        //----------------------rooms
        public void CreateRoom(Room room)
        {
            _context.Rooms.Add(room);
        }

        public IQueryable<Room> GetRooms()
        {
            return _context.Rooms.AsNoTracking();
        }

        public async Task<bool> IsRoomExist(string roomNumber)
        {
            Room? res = await _context.Rooms.FirstOrDefaultAsync((r) => r.RoomNumber == roomNumber);
            if (res == null) return false;
            return true;
        }
        public void UpdateRoom(Room room)
        {
            _context.Rooms.Update(room);
        }
        public async Task<Room?> GetRoomById(int id)
        {
            Room? res = await _context.Rooms.FirstOrDefaultAsync((r) => r.RoomId == id);
            return res;
        }
        public void DeleteRoom(Room room)
        {
            _context.Rooms.Remove(room);
        }
        //----------------------room types

        public async Task<bool> IsRoomTypeExist(string name)
        {
            var res = await _context.RoomTypes.FirstOrDefaultAsync((t) => t.Name == name);
            if (res == null) return false;
            return true;
        }

        public void CreateRoomType(RoomType roomType)
        {
            _context.RoomTypes.Add(roomType);
        }



        public async Task<RoomType?> GetRoomTypeById(int id)
        {
            RoomType? res = await _context.RoomTypes.FirstOrDefaultAsync((r) => r.RoomTypeId == id);
            return res;
        }


        public void DeleteRoomType(RoomType roomType)
        {
            _context.RoomTypes.Remove(roomType);
        }



        public void UpdateRoomType(RoomType roomType)
        {
            _context.RoomTypes.Update(roomType);
        }

        public IQueryable<RoomType> GetRoomTypes()
        {
            return _context.RoomTypes.AsNoTracking();
        }


        //----------------
        public Task<int> saveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
