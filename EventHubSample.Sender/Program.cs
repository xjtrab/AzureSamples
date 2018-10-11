using Microsoft.Azure.EventHubs;
using System;
using System.Text;
using System.Threading.Tasks;

namespace EventHubSample.Sender
{
    class Program
    {
        private static EventHubClient eventHubClient;
        private const string EhConnectionString = "Endpoint=sb://recalleh.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=+1jcOCTtmhCV8lotSxI7G4wYKjGZSW/w=";
        private const string EhEntityPath = "recalleheneity";
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World! Sender");
            MainAsync(args).GetAwaiter().GetResult();
        }

        private static async Task MainAsync(string[] args)
        {
            var connectionStringBuilder = new EventHubsConnectionStringBuilder(EhConnectionString)
            {
                EntityPath = EhEntityPath
            };

            eventHubClient = EventHubClient.CreateFromConnectionString(connectionStringBuilder.ToString());

            await SendEvents(100);

            await eventHubClient.CloseAsync();

            Console.WriteLine("Press ENTER to exit.");
            Console.ReadLine();
        }

        /// <summary>
        /// 创建100个消息事件，异步发送到EventHub
        /// </summary>
        /// <param name="count">个数</param>
        /// <returns></returns>
        private static async Task SendEvents(int count)
        {
            for (var i = 0; i < count; i++)
            {
                try
                {
                    var eventEntity = $"Event {i}";
                    Console.WriteLine($"Sending Event: {eventEntity}");
                    await eventHubClient.SendAsync(new EventData(Encoding.UTF8.GetBytes(eventEntity)));
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"{DateTime.Now} > Exception: {exception.Message}");
                }

                await Task.Delay(10);
            }

            Console.WriteLine($"{count} messages sent.");
        }
    }
}
