using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mango.MessageBus
{
    internal class MessageBus : IMessageBus
    {
        private string connectionString = "";
        public async Task PublishMessage(object message, string topic_queue_Name = "emailshoppingcart")
        {
            var client = new ServiceBusClient(connectionString); 
            ServiceBusSender serviceBusSender = client.CreateSender(topic_queue_Name);

            var jsonMessage = JsonConvert.SerializeObject(message);

            ServiceBusMessage finalMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(jsonMessage))
            {
                CorrelationId = Guid.NewGuid().ToString()
            };

             await serviceBusSender.SendMessageAsync(finalMessage);
            await client.DisposeAsync();

        }
    }
}
