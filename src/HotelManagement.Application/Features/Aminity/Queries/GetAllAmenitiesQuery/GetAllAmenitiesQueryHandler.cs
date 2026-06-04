using HotelManagement.Application.DTOs.Amenities;
using HotelManagement.Application.Interfaces.Repositories;
using HotelManagement.Application.Mapping.Amenities;
using HotelManagement.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Application.Features.Aminity.Queries.GetAllAmenitiesQuery
{
    internal class GetAllAmenitiesQueryHandler : IRequestHandler<GetAllAmenitiesQuery, Result<PagedAmenitiesResponse>>
    {
        private readonly IAmenityRepo _amenityRepo;
        public GetAllAmenitiesQueryHandler(IAmenityRepo amenityRepo)
        {
            this._amenityRepo = amenityRepo;
        }
        public async Task<Result<PagedAmenitiesResponse>> Handle(GetAllAmenitiesQuery request, CancellationToken cancellationToken)
        {
            //get all amenities
            var amenities = _amenityRepo.GetAminities();
            //filter pagenated
            var pagenatedList = amenities.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).ToList();
            var pagenatedResponseList = pagenatedList.ToResponseList();
            int totalCount = await amenities.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

            var pagedResponse = new PagedAmenitiesResponse
            {
                Data = pagenatedResponseList,
                PageSize = request.PageSize,
                Page = request.Page,
                TotalCount = totalCount,
                TotalPages = totalPages,
            };
            // return
            return Result.Success<PagedAmenitiesResponse>(pagedResponse);
        }
    }
}

