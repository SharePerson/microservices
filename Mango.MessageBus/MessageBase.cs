using System;

namespace Mango.MessageBus
{
    public abstract class MessageBase
    {
        public Guid Id { set; get; }
        public DateTime CreationDate { set; get; }
    }
}
