using HotelManagement.Domain.Entities;

namespace HotelManagement.Application.Interfaces.Repositories
{
    public interface IRoomsRepo
    {
        public IQueryable<Room> GetRooms();
        public Task<Room?> GetRoomById(int id);
        public Task<RoomType?> GetRoomTypeById(int id);
        public void CreateRoom(Room room);
        public void DeleteRoom(Room room);
        public void UpdateRoom(Room room);
        public void UpdateRoomType(RoomType roomType);
        public void DeleteRoomType(RoomType roomType);
        public void CreateRoomType(RoomType roomType);
        public Task<bool> IsRoomExist(string roomNumber);
        public Task<bool> IsRoomTypeExist(string name);
        public Task<int> saveChangesAsync();
    }
}
