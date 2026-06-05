using HotelManagement.Application.DTOs.Rooms;
using HotelManagement.Domain.Common;
using HotelManagement.Domain.Enums;
using MediatR;

namespace HotelManagement.Application.Features.Rooms.Queries.GetAllRooms
{
    public record GetAllRoomsQuery(
        int page,
        int pageSize,
        string? RoomTypeName,
        int? Floor,
        string? ViewType,
        RoomStatus? Status,
        decimal? MinPrice,
        decimal? MaxPrice
        ) : IRequest<Result<PagedRoomsResponse>>;
}
