using HotelManagement.Domain.Common;
using HotelManagement.Domain.Enums;
using MediatR;

namespace HotelManagement.Application.Features.Rooms.Commands.Update
{
    public record UpdateRoomCommand
    (
        int RoomId,
     int? RoomTypeId,
     string? RoomNumber,
     int? Floor,
     string? ViewType,
     RoomStatus? Status,
    decimal? PricePerNight,
    string? ImageUrl
) : IRequest<Result<string>>;
}
