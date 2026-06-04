using HotelManagement.Domain.Common;
using MediatR;

namespace HotelManagement.Application.Features.Rooms.Commands.Create
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
