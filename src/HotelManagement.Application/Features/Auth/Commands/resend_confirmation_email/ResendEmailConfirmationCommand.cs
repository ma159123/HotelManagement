using HotelManagement.Domain.Common;
using MediatR;

namespace HotelManagement.Application.Features.Auth.Commands.resend_confirmation_email;

public record ResendEmailConfirmationCommand(
string Email
) : IRequest<Result<string>>;
