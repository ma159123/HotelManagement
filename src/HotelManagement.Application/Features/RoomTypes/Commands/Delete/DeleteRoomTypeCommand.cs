using HotelManagement.Domain.Common;
using MediatR;

namespace HotelManagement.Application.Features.RoomTypes.Commands.Delete
{
    public record DeleteRoomTypeCommand(
     int Id
) : IRequest<Result<string>>;
}
