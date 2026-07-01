using HotelManagement.Application.DTOs.Payments;
using HotelManagement.Domain.Common;
using MediatR;

namespace HotelManagement.Application.Features.PaymentFeature.Commands.PayOrder;

public record InitiatePaymentCommand(int BookingId)
 : IRequest<Result<InitiatePaymentResponse>>;
