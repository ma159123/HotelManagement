namespace HotelManagement.Application.DTOs.Payments
{
    public class PaymobWebhookDto
    {
        public PaymobTransactionDto Obj { get; set; }
        public string Hmac { get; set; }
    }
}
