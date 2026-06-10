using HotelManagement.Application.DTOs.Guest;
using HotelManagement.Domain.Common;
using MediatR;

namespace HotelManagement.Application.Features.GuestFeature.Queries.GetGuestById;

public record GetGuestByIdQuery
(string geustId) : IRequest<Result<GetGuestResponse>>;