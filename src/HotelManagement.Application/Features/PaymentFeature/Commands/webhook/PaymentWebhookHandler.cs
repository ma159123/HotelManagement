using HotelManagement.Application.Features.Bookings.Commands.UpdateBooking;
using HotelManagement.Application.Interfaces.Repositories;
using HotelManagement.Application.Interfaces.Services;
using HotelManagement.Application.Settings;
using HotelManagement.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HotelManagement.Application.Features.PaymentFeature.Commands.webhook
{
    public class PaymentWebhookHandler(
       IPaymobService _paymentService,
       IOptions<PaymobSettings> _paymobSettings,
       IPaymentRepo _repo,
       ISender _sender
       )
       : IRequestHandler<PaymentWebhookCommand, ProcessPaymobWebhookResult>
    {
        public async Task<ProcessPaymobWebhookResult> Handle(
            PaymentWebhookCommand request,
            CancellationToken cancellationToken)
        {
            if (!_paymentService.VerifyWebhookHmac(_paymobSettings.Value.HmacSecret, request.paymobWebhookDto.paymobTransactionDto))
            {
                return new ProcessPaymobWebhookResult(false, "Invalid HMAC");
            }

            var obj = request.paymobWebhookDto.paymobTransactionDto;

            if (obj.Success && !obj.IsPending)
            {
                var payment = _repo.GetAll().AsTracking().Where(x => x.PaymobOrderId == obj.Order.Id).FirstOrDefault();

                if (payment != null)
                {
                    payment.Status = PaymentStatus.Completed;
                    payment.PaymentDate = DateTime.UtcNow;
                    payment.CreatedAt = DateTime.UtcNow;

                    await _sender.Send(new UpdateBookingCommand(payment.BookingId, payment.PaymentId, BookingStatus.Confirmed), cancellationToken);

                    await _repo.SaveChangesAsync(cancellationToken);


                }
                else
                {
                    throw new Exception($"Payment record not found for Order ID: {obj.Order.Id}");
                }
            }

            return new ProcessPaymobWebhookResult(true, "Unpaid");
        }
    }

}
