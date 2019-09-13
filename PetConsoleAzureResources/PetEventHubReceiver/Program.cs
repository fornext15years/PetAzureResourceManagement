using System;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;

namespace PetEventHubReceiver
{
    class Program
    {
        private static string EventHubConnectionString = Environment.GetEnvironmentVariable("EventHubConnectionString", EnvironmentVariableTarget.Machine);
        private static string EventHubName = Environment.GetEnvironmentVariable("EventHubName", EnvironmentVariableTarget.Machine);
        private static string StorageContainerName = Environment.GetEnvironmentVariable("StorageContainer", EnvironmentVariableTarget.Machine);
        private static string StorageAccountName = Environment.GetEnvironmentVariable("StorageAccount", EnvironmentVariableTarget.Machine);
        private static string StorageAccountKey = Environment.GetEnvironmentVariable("StorageAccountKey", EnvironmentVariableTarget.Machine);

        private static readonly string StorageConnectionString = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", StorageAccountName, StorageAccountKey);

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Hello World!");
                MainAsync(args).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now} > Exception: {ex.Message}");
            }
        }

        private static async Task MainAsync(string[] args)
        {
            try
            {
                Console.WriteLine("Registering EventProcessor...");

                var eventProcessorHost = new EventProcessorHost(
                    EventHubName,
                    PartitionReceiver.DefaultConsumerGroupName,
                    EventHubConnectionString,
                    StorageConnectionString,
                    StorageContainerName);

                // Registers the Event Processor Host and starts receiving messages
                await eventProcessorHost.RegisterEventProcessorAsync<SimpleEventProcessor>();

                Console.WriteLine("Receiving. Press ENTER to stop worker.");
                Console.ReadLine();

                // Disposes of the Event Processor Host
                await eventProcessorHost.UnregisterEventProcessorAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now} > Exception: {ex.Message}");
            }

            Console.ReadLine();
        }
    }
}
