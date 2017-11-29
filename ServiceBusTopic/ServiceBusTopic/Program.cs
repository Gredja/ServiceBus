using System;
using Microsoft.ServiceBus.Messaging;

namespace ServiceBusTopic
{
    class Program
    {
        static void Main()
        {
            var connectionString = "Endpoint=sb://gredjasb.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=YnOVED/A4+VFVxdGL/qZBV6kLGYP1zprpHKVxXXGSH0=";
            var topicName = "topic01";

            var client = TopicClient.CreateFromConnectionString(connectionString, topicName);
            var message = new BrokeredMessage("This is a test message (topic)!");
            var subsciption = "gredjasubscription";


            client.Send(message);

            Console.WriteLine($"Message body: {message.GetBody<String>()}");
            Console.WriteLine($"Message id: {message.MessageId}");

            var reciver = SubscriptionClient.CreateFromConnectionString(connectionString, topicName, subsciption);

            Console.WriteLine("Message successfully sent! Press ENTER to exit program");

            reciver.OnMessage(reciveMessage =>
            {
                Console.WriteLine($"Message body: {reciveMessage.GetBody<String>()}");
                Console.WriteLine($"Message id: {reciveMessage.MessageId}");
            });

            Console.ReadKey();

        }
    }
}
