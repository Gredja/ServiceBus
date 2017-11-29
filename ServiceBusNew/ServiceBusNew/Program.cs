using System;
using Microsoft.ServiceBus.Messaging;

namespace ServiceBusNew
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "Endpoint=sb://gredjasb.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=YnOVED/A4+VFVxdGL/qZBV6kLGYP1zprpHKVxXXGSH0=";
            var queueName = "queue01";

            // var client = QueueClient.CreateFromConnectionString(connectionString, queueName);
            var client = QueueClient.CreateFromConnectionString(connectionString, queueName);

            var message = new BrokeredMessage("This is a test message!");

            Console.WriteLine($"Message id: {message.MessageId}");

            client.Send(message);

            client.OnMessage(reciveMessage =>
            {
                Console.WriteLine(($"Message body: { reciveMessage.GetBody<String>()}"));
                Console.WriteLine(($"Message id: {reciveMessage.MessageId}"));
            });

            Console.WriteLine("Press ENTER to exit program");
            Console.ReadLine();
        }
    }
}
