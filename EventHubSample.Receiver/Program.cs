using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;
using System;
using System.Text;
using System.Threading.Tasks;

namespace EventHubSample.Receiver
{
    class Program
    {
        private const string EventHubConnectionString = "Endpoint=sb://recalleh.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=+1jcOCTtmhCLYKjGZSW/w=";
        private const string EventHubName = "recalleheneity";
        private const string StorageContainerName = "blobeh";
        private const string StorageAccountName = "recallstorage";
        private const string StorageAccountKey = "jg/zNdhN4wkotVqbrVIhBWqH5zZwCtx+hiPKfSmxgu9BIZWmycgBBAX4EifznZKXfRQngz6aahQ==";

        private static readonly string StorageConnectionString = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", StorageAccountName, StorageAccountKey);
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World! Receiver");
            MainAsync(args).GetAwaiter().GetResult();
        }

        private static async Task MainAsync(string[] args)
        {
            Console.WriteLine("Registering EventProcessor...");

           
            var eventProcessorHost = new EventProcessorHost(
                EventHubName,
                PartitionReceiver.DefaultConsumerGroupName,
                EventHubConnectionString,
                StorageConnectionString,
                StorageContainerName);

            // Registers the Event Processor Host and starts receiving messages
            await eventProcessorHost.RegisterEventProcessorAsync<SampleEventHubReceiver>();

            Console.WriteLine("Receiving. Press enter key to stop worker.");
            Console.ReadLine();

            // Disposes of the Event Processor Host
            await eventProcessorHost.UnregisterEventProcessorAsync();
        }
    }
}
