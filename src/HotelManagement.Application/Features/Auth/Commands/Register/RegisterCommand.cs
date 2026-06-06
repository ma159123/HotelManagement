using HotelManagement.Application.DTOs.Auth;
using HotelManagement.Domain.Common;
using MediatR;

namespace HotelManagement.Application.Features.Auth.Commands.Register;

public record RegisterCommand(
 string FullName,
 string Email,
 string Password,
 string ConfirmPassword,
 string? Phone,
 string? Address
) : IRequest<Result<RegisterResponse>>;
