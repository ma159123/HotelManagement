using HotelManagement.Application.Interfaces.Repositories;
using HotelManagement.Domain.Common;
using HotelManagement.Domain.Common.Errors;
using MediatR;

namespace HotelManagement.Application.Features.RoomTypes.Commands.Update
{
    internal class UpdateRoomTypeCommandHandler : IRequestHandler<UpdateRoomTypeCommand, Result<string>>
    {
        private readonly IRoomsRepo _roomRepo;
        public UpdateRoomTypeCommandHandler(IRoomsRepo roomsRepo)
        {
            this._roomRepo = roomsRepo;
        }
        public async Task<Result<string>> Handle(UpdateRoomTypeCommand request, CancellationToken cancellationToken)
        {
            //check if room type exist
            var roomType = await _roomRepo.GetRoomTypeById(request.RoomTypeId);
            if (roomType == null)
            {
                return Result.Failure<string>(Error.NotFound);
            }
            // 2. Update properties (manual or mapping)
            roomType.RoomTypeId = request.RoomTypeId;
            if (!string.IsNullOrEmpty(request.Name))
                roomType.Name = request.Name;
            if (request.Capacity.HasValue)
                roomType.Capacity = request.Capacity.Value;
            if (!string.IsNullOrEmpty(request.Amenities))
                roomType.Amenities = request.Amenities;
            if (request.BasePrice.HasValue)
                roomType.BasePrice = request.BasePrice.Value;
            if (!string.IsNullOrEmpty(request.Description))
                roomType.Description = request.Description;

            _roomRepo.UpdateRoomType(roomType);
            await _roomRepo.saveChangesAsync();
            return Result.Success<string>("Updated");
        }
    }
}
