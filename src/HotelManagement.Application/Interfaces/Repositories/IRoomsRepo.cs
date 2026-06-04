using HotelManagement.Domain.Entities;

namespace HotelManagement.Application.Interfaces.Repositories
{
    public interface IRoomsRepo
    {
        //------------------rooms
        public IQueryable<Room> GetRooms();
        public Task<Room?> GetRoomById(int id);

        public void CreateRoom(Room room);
        public void DeleteRoom(Room room);
        public void UpdateRoom(Room room);
        public Task<bool> IsRoomExist(string roomNumber);
        //-----------------------room types
        public IQueryable<RoomType> GetRoomTypes();
        public void UpdateRoomType(RoomType roomType);
        public void DeleteRoomType(RoomType roomType);
        public void CreateRoomType(RoomType roomType);
        public Task<RoomType?> GetRoomTypeById(int id);
        public Task<bool> IsRoomTypeExist(string name);


        //-----------save
        public Task<int> saveChangesAsync();
    }
}
