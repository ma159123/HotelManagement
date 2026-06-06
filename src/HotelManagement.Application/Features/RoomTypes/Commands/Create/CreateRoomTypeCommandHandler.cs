using HotelManagement.Application.Interfaces.Repositories;
using HotelManagement.Application.Mapping.Rooms;
using HotelManagement.Domain.Common;
using HotelManagement.Domain.Common.Errors;
using MediatR;

namespace HotelManagement.Application.Features.RoomTypes.Commands.Create
{
    internal class CreateRoomTypeCommandHandler : IRequestHandler<CreateRoomTypeCommand, Result<string>>
    {
        private readonly IRoomsRepo _roomRepo;
        public CreateRoomTypeCommandHandler(IRoomsRepo roomsRepo)
        {
            this._roomRepo = roomsRepo;
        }

        public async Task<Result<string>> Handle(CreateRoomTypeCommand request, CancellationToken cancellationToken)
        {
            //check if room exist
            bool isExist = await _roomRepo.IsRoomTypeExist(request.Name);
            if (isExist)
            {
                return Result.Failure<string>(Error.AlreadyElementExist);
            }
            //mapping
            var roomType = request.CreateRoomTypeCommandToRoomType();
            _roomRepo.CreateRoomType(roomType);
            await _roomRepo.saveChangesAsync();
            return Result.Success<string>("Created");
        }
    }
}
