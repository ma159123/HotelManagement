using HotelManagement.Application.Interfaces.Repositories;
using HotelManagement.Domain.Common;
using HotelManagement.Domain.Common.Errors;
using MediatR;

namespace HotelManagement.Application.Features.Rooms.Commands.Update
{
    public class UpdateRoomHandler : IRequestHandler<UpdateRoomCommand, Result<string>>

    {
        private readonly IRoomsRepo _roomRepo;
        public UpdateRoomHandler(IRoomsRepo roomsRepo)
        {
            this._roomRepo = roomsRepo;
        }
        public async Task<Result<string>> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
        {
            // 1. Check if room exists
            var room = await _roomRepo.GetRoomById(request.RoomId);
            if (room == null)
                return Result.Failure<string>(Error.NotFound);

            // 2. Update properties (manual or mapping)
            if (request.RoomTypeId.HasValue)
                room.RoomTypeId = request.RoomTypeId.Value;

            if (!string.IsNullOrEmpty(request.RoomNumber))
                room.RoomNumber = request.RoomNumber;

            if (request.Floor.HasValue)
                room.Floor = request.Floor.Value;

            if (!string.IsNullOrEmpty(request.ViewType))
                room.ViewType = request.ViewType;

            if (request.Status.HasValue)
                room.Status = request.Status.Value;

            if (request.PricePerNight.HasValue)
                room.PricePerNight = request.PricePerNight.Value;

            if (request.ImageUrl != null)
                room.ImageUrl = request.ImageUrl;

            // 3. Save
            _roomRepo.UpdateRoom(room);
            await _roomRepo.saveChangesAsync();

            return Result.Success("Updated");
        }
    }
}
