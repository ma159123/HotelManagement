using HotelManagement.Domain.Common;
using MediatR;

namespace HotelManagement.Application.Features.Rooms.Commands.Room.Delete;

public record DeleteRoomCommand
(
     int Id
) : IRequest<Result<string>>;

