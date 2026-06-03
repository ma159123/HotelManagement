using HotelManagement.Application.Features.Rooms.DTOs;
using HotelManagement.Domain.Common;
using MediatR;

namespace HotelManagement.Application.Features.Rooms.Queries.GetRoomById
{
    public record GetRoomByIdQuery : IRequest<Result<RoomResponse>>
    {
        public int Id { get; set; }
        public GetRoomByIdQuery(int id)
        {
            this.Id = id;
        }
    }
}
