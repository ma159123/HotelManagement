namespace HotelManagement.Application.DTOs.Payments
{
    public class PaymobWebhookDto
    {
        public PaymobTransactionDto paymobTransactionDto { get; set; }
        public string Hmac { get; set; }
    }
}
