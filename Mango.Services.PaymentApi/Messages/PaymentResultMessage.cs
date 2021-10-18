using Mango.MessageBus;

namespace Mango.Services.PaymentApi.Messages
{
    public class PaymentResultMessage: MessageBase
    {
        public int OrderId { get; set; }
        public bool Status { get; set; }
    }
}
