using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Mango.MessageBus
{
    public class AzureServiceBusMessageBus : IMessageBus
    {
        private readonly string connectionString = "Endpoint=sb://sitereq.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=oTeXr+TNMDjIJoaR31YHMoFCVyb0Vw/ifmd4l3TJH+I=";

        public async Task PublishMessage(MessageBase message, string topic)
        {
            ServiceBusClient client = null;
            ServiceBusSender sender = null;

            try
            {
                client = new(connectionString);
                sender = client.CreateSender(topic);
                await sender.SendMessageAsync(new ServiceBusMessage(JsonConvert.SerializeObject(message)));
            }
            finally
            {
                if(client != null)
                {
                    await client.DisposeAsync();
                }
                
                if(sender != null)
                {
                    await sender.DisposeAsync();
                }
                
            }
        }
    }
}
