using HotelManagement.Domain.Common;
using MediatR;

namespace HotelManagement.Application.Features.RoomTypes.Commands.Create
{
    public record CreateRoomTypeCommand
    (
         string Name,
         string? Description,
         int Capacity,
         decimal BasePrice,
         string? Amenities
    ) : IRequest<Result<string>>;
}
