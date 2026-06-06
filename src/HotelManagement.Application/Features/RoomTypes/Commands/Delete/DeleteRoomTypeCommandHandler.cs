using HotelManagement.Application.Interfaces.Repositories;
using HotelManagement.Domain.Common;
using HotelManagement.Domain.Common.Errors;
using MediatR;

namespace HotelManagement.Application.Features.RoomTypes.Commands.Delete
{
    internal class DeleteRoomTypeCommandHandler : IRequestHandler<DeleteRoomTypeCommand, Result<string>>
    {
        private readonly IRoomsRepo _roomRepo;
        public DeleteRoomTypeCommandHandler(IRoomsRepo roomsRepo)
        {
            this._roomRepo = roomsRepo;
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