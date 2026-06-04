using HotelManagement.Application.Features.Rooms.DTOs;
using HotelManagement.Application.Interfaces.Repositories;
using HotelManagement.Application.Mapping.Rooms;
using HotelManagement.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Application.Features.Rooms.Queries.GetAllRoomTypesQuery
{
    internal class GetAllRoomTypesHandler : IRequestHandler<GetAllRoomTypesQuery, Result<PagedRoomTypeResponse>>
    {
        private readonly IRoomsRepo _roomsRepo;
        public GetAllRoomTypesHandler(IRoomsRepo roomsRepo)
        {
            this._roomsRepo = roomsRepo;
        }
        public async Task<Result<PagedRoomTypeResponse>> Handle(GetAllRoomTypesQuery request, CancellationToken cancellationToken)
        {
            //get all room types
            var roomTypes = _roomsRepo.GetRoomTypes();
            //filter pagenated
            var pagenatedList = roomTypes.Skip((request.page - 1) * request.pageSize).Take(request.pageSize).ToList();
            var pagenatedResponseList = pagenatedList.ToResponseList();
            int totalCount = await roomTypes.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)request.pageSize);

            var pagedResponse = new PagedRoomTypeResponse
            {
                Data = pagenatedResponseList,
                PageSize = request.pageSize,
                Page = request.page,
                TotalCount = totalCount,
                TotalPages = totalPages,
            };
            // return
            return Result.Success<PagedRoomTypeResponse>(pagedResponse);
        }
    }
}

