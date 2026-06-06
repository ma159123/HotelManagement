using HotelManagement.Domain.Common;
using MediatR;

namespace HotelManagement.Application.Features.Aminity.Commands.Create
{
    public record CreateAminityCommand
   (
     int AmenityId,
     string Name,
     string Description,
     string IconUrl
) : IRequest<Result<string>>;
}
