using Mango.MessageBus;

namespace Mango.Services.OrderApi.Messages
{
    public class PaymentRequestMessage: MessageBase
    {
        public int OrderId { get; set; }
        public string Name { get; set; }
        public string CardNumber { get; set; }
        public string CVV { get; set; }
        public string MMYY { get; set; }
        public double OrderTotal { set; get; }
    }
}
