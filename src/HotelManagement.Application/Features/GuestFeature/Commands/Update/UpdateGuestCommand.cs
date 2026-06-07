using HotelManagement.Domain.Common;
using MediatR;

namespace HotelManagement.Application.Features.GuestFeature.Commands.Update
{
    public record UpdateGuestCommand
    (
        string GuestId,
         string? FullName,
         string? Phone,
         string? Address
        ) : IRequest<Result<string>>;
}
