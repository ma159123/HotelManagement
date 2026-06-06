using HotelManagement.Application.DTOs.Auth;
using HotelManagement.Domain.Common;
using MediatR;

namespace HotelManagement.Application.Features.Auth.Commands.Login;

public record LoginCommand(
 string Email,
 string Password
) : IRequest<Result<LoginResponse>>;
