using HotelManagement.Application.Features.Rooms.DTOs;
using HotelManagement.Application.Interfaces.Repositories;
using HotelManagement.Application.Mapping.Rooms;
using HotelManagement.Domain.Common;
using MediatR;

namespace HotelManagement.Application.Features.Rooms.Queries.GetAllRooms
{
    public class GetAllRoomsQueryHandler : IRequestHandler<GetAllRoomsQuery, Result<List<RoomResponse>>>
    {
        private readonly IRoomsRepo _roomsRepo;
        public GetAllRoomsQueryHandler(IRoomsRepo roomsRepo)
        {
            this._roomsRepo = roomsRepo;
        }
        public async Task<Result<List<RoomResponse>>> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
        {
            //get all rooms
            var rooms = _roomsRepo.GetRooms().ToList();
            //map list 
            var result = rooms.ToResponseList();
            // return
            return Result.Success<List<RoomResponse>>(result);
        }
    }
}
