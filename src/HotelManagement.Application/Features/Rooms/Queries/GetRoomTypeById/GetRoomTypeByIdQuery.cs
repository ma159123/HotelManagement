using HotelManagement.Application.Features.Rooms.DTOs;
using HotelManagement.Domain.Common;
using MediatR;

namespace HotelManagement.Application.Features.Rooms.Queries.GetRoomTypeById
{
    public class GetRoomTypeByIdQuery : IRequest<Result<RoomTypeResponse>>
    {
        public int Id { get; set; }
        public GetRoomTypeByIdQuery(int id)
        {
            this.Id = id;
        }
    }
}
