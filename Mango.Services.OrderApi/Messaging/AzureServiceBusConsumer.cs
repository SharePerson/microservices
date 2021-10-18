using Azure.Messaging.ServiceBus;
using Mango.MessageBus;
using Mango.Services.OrderApi.Messages;
using Mango.Services.OrderApi.Models;
using Mango.Services.OrderApi.Repositories;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Services.OrderApi.Messaging
{
    public class AzureServiceBusConsumer: IAzureServiceBusConsumer
    {
        private readonly string serviceBusConnectionString;
        private readonly string checkoutMessageTopic;
        private readonly string checkoutMessageSubscription;
        private readonly string orderPaymentProcessTopic;
        private readonly string orderPaymentResultTopic;

        private readonly OrderRepository _orderRepository;
        private readonly IConfiguration _configuration;

        private readonly ServiceBusProcessor checkoutProcessor;
        private readonly ServiceBusProcessor orderPaymentResultProcessor;
        private readonly IMessageBus _messageBus;

        public AzureServiceBusConsumer(OrderRepository orderRepository, IConfiguration configuration, IMessageBus messageBus)
        {
            _orderRepository = orderRepository;
            _configuration = configuration;

            serviceBusConnectionString = _configuration.GetValue<string>("ServiceBusConnectionString");
            checkoutMessageTopic = _configuration.GetValue<string>("CheckoutMessageTopic");
            checkoutMessageSubscription = _configuration.GetValue<string>("CheckoutMessageSubscription");
            orderPaymentProcessTopic = _configuration.GetValue<string>("OrderPaymentProcessTopic");
            orderPaymentResultTopic = _configuration.GetValue<string>("OrderPaymentResultTopic");


            var client = new ServiceBusClient(serviceBusConnectionString);

            //processors can be used on topics/subscriptions or queues
            checkoutProcessor = client.CreateProcessor(checkoutMessageTopic, checkoutMessageSubscription);
            orderPaymentResultProcessor = client.CreateProcessor(orderPaymentResultTopic, checkoutMessageSubscription);
            _messageBus = messageBus;
        }

        public async Task Start()
        {
            checkoutProcessor.ProcessMessageAsync += OnCheckoutMessageReceived;
            checkoutProcessor.ProcessErrorAsync += ErrorHandler;
            await checkoutProcessor.StartProcessingAsync();

            orderPaymentResultProcessor.ProcessMessageAsync += OnPaymentResultMessageReceived;
            orderPaymentResultProcessor.ProcessErrorAsync += ErrorHandler;
            await orderPaymentResultProcessor.StartProcessingAsync();
        }

        public async Task Stop()
        {
            await checkoutProcessor.StopProcessingAsync();
            await checkoutProcessor.DisposeAsync();

            await orderPaymentResultProcessor.StopProcessingAsync();
            await orderPaymentResultProcessor.DisposeAsync();
        }

        private async Task OnPaymentResultMessageReceived(ProcessMessageEventArgs args)
        {
            ServiceBusReceivedMessage message = args.Message;
            string content = Encoding.UTF8.GetString(message.Body);
            PaymentResultMessage messageRecieved = JsonConvert.DeserializeObject<PaymentResultMessage>(content);

            await _orderRepository.UpdateOrderPaymentStatus(messageRecieved.OrderId, messageRecieved.Status);
            await args.CompleteMessageAsync(args.Message);
        }

        private async Task OnCheckoutMessageReceived(ProcessMessageEventArgs args)
        {
            ServiceBusReceivedMessage message = args.Message;
            string content = Encoding.UTF8.GetString(message.Body);
            CheckoutMessage messageRecieved = JsonConvert.DeserializeObject<CheckoutMessage>(content);

            if(messageRecieved != null)
            {
                OrderHeader orderHeader = new()
                {
                    UserId = messageRecieved.UserId,
                    CardNumber = messageRecieved.CardNumber,
                    CouponCode = messageRecieved.CouponCode,
                    CVV = messageRecieved.CVV,
                    DiscountTotal = messageRecieved.DiscountTotal,
                    Email = messageRecieved.Email,
                    FirstName = messageRecieved.FirstName,
                    LastName = messageRecieved.LastName,
                    MMYY = messageRecieved.MMYY,
                    OrderDetails = new List<OrderDetail>(),
                    OrderTime = DateTime.Now,
                    OrderTotal = messageRecieved.OrderTotal,
                    PaymentStatus = false,
                    Phone = messageRecieved.Phone,
                    PickupTime = messageRecieved.PickupTime
                };

                if(messageRecieved.CartDetails != null && messageRecieved.CartDetails.Any())
                {
                    foreach(CartDetailDto cartDetail in messageRecieved.CartDetails)
                    {
                        OrderDetail orderDetail = new()
                        {
                            Count = cartDetail.Count,
                            Price = cartDetail.Product?.Price ?? 0,
                            ProductName = cartDetail.Product?.Name ?? string.Empty,
                            ProductId = cartDetail.Product?.Id ?? 0,
                            OrderHeaderId = orderHeader.Id
                        };

                        orderHeader.TotalItems += orderDetail.Count;
                        orderHeader.OrderDetails.Add(orderDetail);
                    }

                    await _orderRepository.CreateUpdate(orderHeader);

                    PaymentRequestMessage paymentRequestMessage = new()
                    {
                        CardNumber = orderHeader.CardNumber,
                        CVV = orderHeader.CVV,
                        MMYY = orderHeader.MMYY,
                        Name = orderHeader.FirstName + " " + orderHeader.LastName,
                        OrderId = orderHeader.Id,
                        OrderTotal = orderHeader.OrderTotal
                    };

                    try
                    {
                        await _messageBus.PublishMessage(paymentRequestMessage, orderPaymentProcessTopic);
                        await args.CompleteMessageAsync(args.Message);
                    }
                    catch
                    {
                        //should log the error
                        throw;
                    }
                }
            }
        }

        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }
    }
}
