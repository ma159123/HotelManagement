namespace HotelManagement.Application.DTOs.Payments
{
    public class PaymobTransactionDto
    {
        public string Id { get; set; }              // Transaction ID
        public bool Success { get; set; }
        public bool IsPending { get; set; }
        public decimal AmountCents { get; set; }
        public PaymobOrderDto Order { get; set; }
    }
}
