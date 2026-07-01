using HotelManagement.Application.DTOs.Payments;
using HotelManagement.Application.Interfaces.Repositories;
using HotelManagement.Application.Interfaces.Services;
using HotelManagement.Application.Settings;
using HotelManagement.Domain.Common;
using HotelManagement.Domain.Common.Errors;
using HotelManagement.Domain.Entities;
using HotelManagement.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Options;

namespace HotelManagement.Application.Features.PaymentFeature.Commands.PayOrder
{
    public class InitiatePaymentHandler : IRequestHandler<InitiatePaymentCommand, Result<InitiatePaymentResponse>>
    {
        private readonly IPaymobService _paymobService;
        private readonly IPaymentRepo _paymentRepo;
        private readonly IBookingService _bookingService;
        private readonly PaymobSettings _settings;
        private readonly ICurrentUserService _currentUserService;

        public InitiatePaymentHandler(IBookingService bookingService, IPaymobService paymobService, IPaymentRepo paymentRepo, IOptions<PaymobSettings> paymobSettings, ICurrentUserService currentUserService)
        {
            _bookingService = bookingService;
            _paymobService = paymobService;
            _paymentRepo = paymentRepo;
            _currentUserService = currentUserService;
            _settings = paymobSettings.Value;

        }
        public async Task<Result<InitiatePaymentResponse>> Handle(
            InitiatePaymentCommand request,
            CancellationToken cancellationToken)
        {
            // 1 - get Booking
            var booking = await _bookingService.GetBookingByIdAsync(request.BookingId);

            if (booking == null)
                return Result.Failure<InitiatePaymentResponse>(Error.NotFound);

            // 2 - insure Booking owned to Guest 
            if (booking.GuestId != _currentUserService.UserId)
                return Result.Failure<InitiatePaymentResponse>(
                    new Error("Payment.Unauthorized", "Unauthorized.", 403));

            // 3 - insure Booking still Pending
            if (booking.Status != BookingStatus.Pending)
                return Result.Failure<InitiatePaymentResponse>(
                    new Error("Payment.InvalidStatus",
                        "Booking is not pending payment.", 400));

            // 4 - make sure Payment not exist
            var existingPayment = await _paymentRepo.GetPaymentById(request.BookingId);

            if (existingPayment?.Status == PaymentStatus.Completed)
                return Result.Failure<InitiatePaymentResponse>(
                    new Error("Payment.AlreadyPaid", "Booking already paid.", 400));

            // 5 - Paymob Flow
            var authToken = await _paymobService.GetAuthTokenAsync(cancellationToken);

            var paymobOrderId = await _paymobService.RegisterOrderAsync(
                authToken, booking.BookingId, booking.TotalPrice, cancellationToken);

            var paymentKey = await _paymobService.GetPaymentKeyAsync(
                authToken, paymobOrderId, booking.TotalPrice, booking, cancellationToken);

            // 6 - save Payment Pending state
            var payment = existingPayment ?? new Payment
            {
                BookingId = booking.BookingId,
                Amount = booking.TotalPrice,
            };

            payment.PaymobOrderId = paymobOrderId;
            payment.PaymentKey = paymentKey;
            payment.Status = PaymentStatus.Pending;

            if (existingPayment == null)
                await _paymentRepo.AddAsync(payment, cancellationToken);

            await _paymentRepo.SaveChangesAsync(cancellationToken);

            // 7 - return Payment URL
            var paymentUrl =
                $"https://accept.paymob.com/api/acceptance/iframes/{_settings.IframeId}" +
                $"?payment_token={paymentKey}";

            return Result.Success(new InitiatePaymentResponse(
                paymentUrl,
                paymobOrderId,
                payment.PaymentId
            ));
        }
    }
}
