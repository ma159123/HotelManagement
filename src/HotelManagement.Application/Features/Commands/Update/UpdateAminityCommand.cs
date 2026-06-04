using HotelManagement.Domain.Common;
using MediatR;

namespace HotelManagement.Application.Features.Aminity.Commands.Update
{
    public record UpdateAminityCommand
    (
         int AmenityId,
     string? Name,
     string? Description,
     string? IconUrl
        ) : IRequest<Result<string>>;
}
