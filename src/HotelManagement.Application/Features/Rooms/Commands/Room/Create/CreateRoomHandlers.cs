using HotelManagement.Application.Interfaces.Repositories;
using HotelManagement.Application.Mapping.Rooms;
using HotelManagement.Domain.Common;
using HotelManagement.Domain.Common.Errors;
using MediatR;

namespace HotelManagement.Application.Features.Rooms.Commands.Room.Create
{
    internal class CreateRoomHandlers : IRequestHandler<CreateRoomCommand, Result<string>>
                                        , IRequestHandler<CreateRoomTypeCommand, Result<string>>
    {
        private readonly IRoomsRepo _roomRepo;
        public CreateRoomHandlers(IRoomsRepo roomsRepo)
        {
            this._roomRepo = roomsRepo;
        }
        public async Task<Result<string>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            //check if room exist
            bool isExist = await _roomRepo.IsRoomExist(request.RoomNumber);
            if (isExist)
            {
                return Result.Failure<string>(Error.AlreadyElementExist);
            }
            //mapping
            var room = request.CreateRoomCommandToRoom();
            _roomRepo.CreateRoom(room);
            await _roomRepo.saveChangesAsync();
            return Result.Success<string>("Created");
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
