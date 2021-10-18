using System;

namespace Mango.MessageBus
{
    public abstract class MessageBase
    {
        public MessageBase()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.Now;
        }

        public Guid Id { private set; get; }
        public DateTime CreationDate { private set; get; }
    }
}
