using HotelManagement.Application.Features.Rooms.DTOs;
using HotelManagement.Domain.Common;
using MediatR;

namespace HotelManagement.Application.Features.Rooms.Queries.GetAllRooms
{
    public record GetAllRoomsQuery : IRequest<Result<List<RoomResponse>>>;
}
