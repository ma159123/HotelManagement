using HotelManagement.Application.Interfaces.Repositories;
using HotelManagement.Application.Interfaces.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelManagement.Application.Features.PaymentFeature.Commands.webhook
{
    public class PaymentWebhookHandler(
       IPaymobService _paymentService,
       IPaymentRepo _repo,
       ISender _sender
       )
       : IRequestHandler<PaymentWebhookCommand, ProcessPaymobWebhookResult>
    {
        public async Task<ProcessPaymobWebhookResult> Handle(
            PaymentWebhookCommand request,
            CancellationToken cancellationToken)
        {
            if (!_paymentService.VerifyWebhookHmac(request.Callback, request.Hmac))
            {
                _logger.LogWarning("Invalid HMAC received for Paymob callback");
                return new ProcessPaymobCallbackResult(false, "Invalid HMAC");
            }

            var obj = request.Callback.Obj;

            _logger.LogInformation(
                "Paymob callback received — TransactionId: {TxId}, Success: {Success}, Pending: {Pending}",
                obj.Id, obj.Success, obj.Pending);

            if (obj.Success
                            && !obj.Pending
                            && !obj.ErrorOccured
                            && !obj.IsAuth
                            && !obj.IsRefund
                            && !obj.IsVoided
                            && obj.IsStandalonePayment)
            {
                var payment = _repo.GetAll().AsTracking().Where(x => x.GatewayOrderId == obj.Order.Id).FirstOrDefault();

                if (payment != null)
                {
                    payment.Status = "Paid";
                    payment.Confirmed = true;
                    payment.PaidAt = DateTime.UtcNow;
                    payment.UpdatedAt = DateTime.UtcNow;
                    payment.FailureReason = null;

                    await _sender.Send(new ConfirmBookingCommand(payment.BookingId, payment.Id), cancellationToken);

                    await _repo.SaveChangesAsync(cancellationToken);


                }
                else
                {
                    _logger.LogError("Payment record not found for Paymob callback with Order ID: {OrderId}", obj.Order.Id);
                    throw new Exception($"Payment record not found for Order ID: {obj.Order.Id}");
                }
            }

            return new ProcessPaymobCallbackResult(true, "Unpaid");
        }
    }

}
