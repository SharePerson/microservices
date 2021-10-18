using Azure.Messaging.ServiceBus;
using Mango.MessageBus;
using Mango.Services.PaymentApi.Messages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PaymentProcessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Services.PaymentApi.Messaging
{
    public class AzureServiceBusConsumer: IAzureServiceBusConsumer
    {
        private readonly string serviceBusConnectionString;
        private readonly string orderPaymentProcessSubscription;
        private readonly string orderPaymentProcessTopic;
        private readonly string orderPaymentResultTopic;

        private readonly IConfiguration _configuration;
        private readonly IProcessPayment _processPayment;

        private readonly ServiceBusProcessor orderPaymentProcessor;
        private readonly IMessageBus _messageBus;

        public AzureServiceBusConsumer(IConfiguration configuration, IMessageBus messageBus, IProcessPayment processPayment)
        {
            _processPayment = processPayment;
            _configuration = configuration;

            serviceBusConnectionString = _configuration.GetValue<string>("ServiceBusConnectionString");
            orderPaymentProcessSubscription = _configuration.GetValue<string>("OrderPaymentProcessSubscription");
            orderPaymentProcessTopic = _configuration.GetValue<string>("OrderPaymentProcessTopic");
            orderPaymentResultTopic = _configuration.GetValue<string>("OrderPaymentResultTopic");


            var client = new ServiceBusClient(serviceBusConnectionString);
            orderPaymentProcessor = client.CreateProcessor(orderPaymentProcessTopic, orderPaymentProcessSubscription);
            _messageBus = messageBus;
        }

        public async Task Start()
        {
            orderPaymentProcessor.ProcessMessageAsync += ProcessPayment;
            orderPaymentProcessor.ProcessErrorAsync += ErrorHandler;

            await orderPaymentProcessor.StartProcessingAsync();
        }

        public async Task Stop()
        {
            await orderPaymentProcessor.StopProcessingAsync();
            await orderPaymentProcessor.DisposeAsync();
        }

        private async Task ProcessPayment(ProcessMessageEventArgs args)
        {
            ServiceBusReceivedMessage message = args.Message;
            string content = Encoding.UTF8.GetString(message.Body);
            PaymentRequestMessage paymentRequestMessage = JsonConvert.DeserializeObject<PaymentRequestMessage>(content);

            bool paymentIsProcessed = _processPayment.PaymentProcessor();

            PaymentResultMessage paymentResultMessage = new()
            {
                OrderId = paymentRequestMessage.OrderId,
                Status = paymentIsProcessed
            };

            try
            {
                await _messageBus.PublishMessage(paymentResultMessage, orderPaymentResultTopic);
                await args.CompleteMessageAsync(args.Message);
            }
            catch
            {
                //should log errors here
                throw;
            }
        }

        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }
    }
}
