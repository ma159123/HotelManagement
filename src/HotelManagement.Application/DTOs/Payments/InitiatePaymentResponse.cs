namespace HotelManagement.Application.DTOs.Payments;

public record InitiatePaymentResponse(
 string PaymentUrl,
 string PaymobOrderId,
 int PaymentId
);
