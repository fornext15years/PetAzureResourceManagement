using Microsoft.Azure.EventHubs;
using System;
using System.Text;
using System.Threading.Tasks;

namespace PetEventHubSender
{
    class Program
    {
        private static EventHubClient eventHubClient;
        

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            MainAsync().GetAwaiter();

            Console.ReadKey();
        }

        private static async Task MainAsync()
        {
            string EventHubConnectionString = Environment.GetEnvironmentVariable("EventHubConnectionString", EnvironmentVariableTarget.Machine);
            string EventHubName = Environment.GetEnvironmentVariable("EventHubName", EnvironmentVariableTarget.Machine);

            var connectionStringBuilder = new EventHubsConnectionStringBuilder(EventHubConnectionString)
            {
                EntityPath = EventHubName
            };

            eventHubClient = EventHubClient.CreateFromConnectionString(connectionStringBuilder.ToString());
            await SendMessagesToEventHub(100);
            await eventHubClient.CloseAsync();
        }

        private static async Task SendMessagesToEventHub(int numMessagesToSend)
        {
            for(var i=0; i<numMessagesToSend; i++)
            {
                try
                {
                    var message = $"Message {i}";
                    Console.WriteLine($"Sending message: {message}");
                    await eventHubClient.SendAsync(new EventData(Encoding.UTF8.GetBytes(message)));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{DateTime.Now} > Exception: {ex.Message}");
                }

                await Task.Delay(10);
            }

            Console.WriteLine($"{numMessagesToSend} messages sent.");
        }
    }
}
