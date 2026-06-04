using HotelManagement.Application.DTOs.Amenities;
using HotelManagement.Domain.Common;
using MediatR;

namespace HotelManagement.Application.Features.Aminity.Queries.GetAllAmenitiesQuery
{
    public record GetAllAmenitiesQuery
    (
        int Page,
        int PageSize
        ) : IRequest<Result<PagedAmenitiesResponse>>;
}
