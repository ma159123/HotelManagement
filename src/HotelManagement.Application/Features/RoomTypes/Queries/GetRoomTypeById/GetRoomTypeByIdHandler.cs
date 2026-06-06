using HotelManagement.Application.Features.Rooms.DTOs;
using HotelManagement.Application.Interfaces.Repositories;
using HotelManagement.Application.Mapping.Rooms;
using HotelManagement.Domain.Common;
using HotelManagement.Domain.Common.Errors;
using MediatR;

namespace HotelManagement.Application.Features.RoomTypes.Queries.GetRoomTypeById
{
    internal class GetRoomTypeByIdHandler : IRequestHandler<GetRoomTypeByIdQuery, Result<RoomTypeResponse>>
    {
        private readonly IRoomsRepo _roomsRepo;
        public GetRoomTypeByIdHandler(IRoomsRepo roomsRepo)
        {
            this._roomsRepo = roomsRepo;
        }
        public async Task<Result<RoomTypeResponse>> Handle(GetRoomTypeByIdQuery request, CancellationToken cancellationToken)
        {
            //get room
            var roomType = await _roomsRepo.GetRoomTypeById(request.Id);
            if (roomType == null)
            {
                return Result.Failure<RoomTypeResponse>(Error.NotFound);
            }
            //map list 
            var result = roomType!.ToResponse();
            // return
            return Result.Success<RoomTypeResponse>(result);
        }
    }
}