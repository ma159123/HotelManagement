using HotelManagement.Domain.Common;
using HotelManagement.Domain.Enums;
using MediatR;

namespace HotelManagement.Application.Features.GuestFeature.GuestProfile.Update
{
    public record UpdateGuestProfileCommand
    (
           int ProfileId,
            string? GuestId,
            IdType? IdType,
            string? IdNumber,
            string? nationality,
            DateTime? DateOfBirth
        ) : IRequest<Result<string>>;
}
