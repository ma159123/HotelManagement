using HotelManagement.Application.DTOs.Payments;
using MediatR;

namespace HotelManagement.Application.Features.PaymentFeature.Commands.webhook
{
    public record PaymentWebhookCommand
    (
        PaymobWebhookDto paymobWebhookDto
    ) : IRequest<ProcessPaymobWebhookResult>;

}

public record ProcessPaymobWebhookResult(bool Success, string Message);