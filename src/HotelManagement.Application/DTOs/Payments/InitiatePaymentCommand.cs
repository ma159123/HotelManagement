using HotelManagement.Domain.Common;
using MediatR;

namespace HotelManagement.Application.DTOs.Payments;

public record InitiatePaymentCommand(int BookingId)
 : IRequest<Result<InitiatePaymentResponse>>;
