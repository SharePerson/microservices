namespace Mango.Services.OrderApi.Messages
{
    public class PaymentResultMessage
    {
        public int OrderId { get; set; }
        public bool Status { get; set; }
    }
}
