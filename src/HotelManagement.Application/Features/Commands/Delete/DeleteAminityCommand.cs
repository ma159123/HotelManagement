using HotelManagement.Domain.Common;
using MediatR;

namespace HotelManagement.Application.Features.Aminity.Commands.Delete;

public record DeleteAminityCommand
(int Id) : IRequest<Result<string>>;
