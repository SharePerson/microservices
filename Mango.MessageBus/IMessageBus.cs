using System.Threading.Tasks;

namespace Mango.MessageBus
{
    public interface IMessageBus
    {
        Task PublishMessage(MessageBase message, string topic);
    }
}
