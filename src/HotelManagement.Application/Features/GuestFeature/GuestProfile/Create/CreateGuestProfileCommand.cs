using HotelManagement.Domain.Common;
using HotelManagement.Domain.Enums;
using MediatR;

namespace HotelManagement.Application.Features.GuestFeature.GuestProfile.Create;

public record CreateGuestProfileCommand(
 string GuestId,
 IdType IdType,
 string IdNumber,
 string nationality,
 DateTime DateOfBirth
    ) : IRequest<Result<string>>;
