using HotelManagement.Domain.Common;
using MediatR;

namespace HotelManagement.Application.Features.Auth.Commands.ChangePassword;

public record ChangePasswordCommand(
    string Id,
 string CurrentPassword,
 string NewPassword,
 string ConfirmNewPassword
) : IRequest<Result<string>>;
