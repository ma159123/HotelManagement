using HotelManagement.Application.Features.Rooms.DTOs;
using HotelManagement.Application.Interfaces.Repositories;
using HotelManagement.Application.Mapping.Rooms;
using HotelManagement.Domain.Common;
using HotelManagement.Domain.Common.Errors;
using MediatR;

namespace HotelManagement.Application.Features.Rooms.Queries.GetRoomById
{
    internal class GetRoomByIdQueryHandler : IRequestHandler<GetRoomByIdQuery, Result<RoomResponse>>
    {
        private readonly IRoomsRepo _roomsRepo;
        public GetRoomByIdQueryHandler(IRoomsRepo roomsRepo)
        {
            this._roomsRepo = roomsRepo;
        }
        public async Task<Result<RoomResponse>> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
        {
            //get room
            var room = await _roomsRepo.GetRoomById(request.Id);
            if (room == null)
            {
                return Result.Failure<RoomResponse>(Error.NotFound);
            }
            //map list 
            var result = room!.ToResponse();
            // return
            return Result.Success<RoomResponse>(result);
        }
    }
}