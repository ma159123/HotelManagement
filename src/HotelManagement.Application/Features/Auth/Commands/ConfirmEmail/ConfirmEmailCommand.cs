using HotelManagement.Domain.Common;
using MediatR;

namespace HotelManagement.Application.Features.Auth.Commands.ConfirmEmail;

public record ConfirmEmailCommand(
 string Email,
 string Token
) : IRequest<Result<string>>;
