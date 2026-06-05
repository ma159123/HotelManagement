using HotelManagement.Application.DTOs.Rooms;
using HotelManagement.Application.Features.Rooms.DTOs;
using HotelManagement.Application.Interfaces.Repositories;
using HotelManagement.Domain.Common;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Application.Features.Rooms.Queries.GetAllRooms
{
    public class GetAllRoomsQueryHandler : IRequestHandler<GetAllRoomsQuery, Result<PagedRoomsResponse>>
    {
        private readonly IRoomsRepo _roomsRepo;
        public GetAllRoomsQueryHandler(IRoomsRepo roomsRepo)
        {
            this._roomsRepo = roomsRepo;
        }
        public async Task<Result<PagedRoomsResponse>> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
        {
            //get all rooms
            var rooms = _roomsRepo.GetRooms();
            //filtering
            if (request.Status != null)
                rooms = rooms.Where((r) => r.Status == request.Status);
            if (request.MinPrice != null)
                rooms = rooms.Where(r => r.PricePerNight >= request.MinPrice);
            if (request.MaxPrice != null)
                rooms = rooms.Where(r => r.PricePerNight <= request.MaxPrice);
            if (request.Floor != null)
                rooms = rooms.Where((r) => r.Floor == request.Floor);
            if (request.ViewType != null)
                rooms = rooms.Where((r) => r.ViewType == request.ViewType);
            if (request.RoomTypeName != null)
                rooms = rooms.Where((r) => r.RoomType.Name == request.RoomTypeName);


            //pagination
            int totalCount = await rooms.CountAsync(cancellationToken);
            var totalPages = (int)Math.Ceiling(totalCount / (double)request.pageSize);
            int skip = (request.page - 1) * request.pageSize;

            var roomsList = await rooms.Skip(skip)
                                    .Take(request.pageSize)
                                    .ToListAsync(cancellationToken);
            //map list 
            var result = roomsList.Adapt<List<RoomResponse>>();

            var pagedResponse = new PagedRoomsResponse
            {
                Data = result,
                PageSize = request.pageSize,
                Page = request.page,
                TotalCount = totalCount,
                TotalPages = totalPages,
            };

            // return
            return Result.Success(pagedResponse);
        }
    }
}
