using HotelManagement.Domain.Common;
using MediatR;

namespace HotelManagement.Application.Features.Rooms.Commands.Room.Update
{
    public record UpdateRoomTypeCommand
     (
        int RoomTypeId,
         string? Name,
         string? Description,
         int? Capacity,
         decimal? BasePrice,
         string? Amenities
    ) : IRequest<Result<string>>;
}
