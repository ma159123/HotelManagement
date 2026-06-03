using HotelManagement.Domain.Common;
using MediatR;

namespace HotelManagement.Application.Features.Rooms.Commands.Room.Delete
{
    public record DeleteRoomTypeCommand(
     int Id
) : IRequest<Result<string>>;
}
