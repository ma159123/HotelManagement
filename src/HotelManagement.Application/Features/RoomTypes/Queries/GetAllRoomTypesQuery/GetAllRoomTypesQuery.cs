using HotelManagement.Application.Features.Rooms.DTOs;
using HotelManagement.Domain.Common;
using MediatR;

namespace HotelManagement.Application.Features.RoomTypes.Queries.GetAllRoomTypesQuery
{
    public record GetAllRoomTypesQuery
    (
        int page,
        int pageSize
    ) : IRequest<Result<PagedRoomTypeResponse>>;
}
