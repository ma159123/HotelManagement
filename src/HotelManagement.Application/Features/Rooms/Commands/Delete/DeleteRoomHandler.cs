using HotelManagement.Application.Interfaces.Repositories;
using HotelManagement.Domain.Common;
using HotelManagement.Domain.Common.Errors;
using MediatR;

namespace HotelManagement.Application.Features.Rooms.Commands.Delete
{
    internal class DeleteRoomHandler : IRequestHandler<DeleteRoomCommand, Result<string>>,
                                       IRequestHandler<DeleteRoomTypeCommand, Result<string>>
    {
        private readonly IRoomsRepo _roomRepo;
        public DeleteRoomHandler(IRoomsRepo roomsRepo)
        {
            this._roomRepo = roomsRepo;
        }
        public async Task<Result<string>> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
        {
            //check if room exist
            var room = await _roomRepo.GetRoomById(request.Id);
            if (room == null)
            {
                return Result.Failure<string>(Error.NotFound);
            }

            _roomRepo.DeleteRoom(room);
            await _roomRepo.saveChangesAsync();
            return Result.Success<string>("Deleted");
        }

        public async Task<Result<string>> Handle(DeleteRoomTypeCommand request, CancellationToken cancellationToken)
        {
            //check if room exist
            var room = await _roomRepo.GetRoomTypeById(request.Id);
            if (room == null)
            {
                return Result.Failure<string>(Error.NotFound);
            }

            _roomRepo.DeleteRoomType(room);
            await _roomRepo.saveChangesAsync();
            return Result.Success<string>("Deleted");
        }
    }
}