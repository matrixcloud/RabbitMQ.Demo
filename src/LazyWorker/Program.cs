using RabbitMQ.Demo.Common;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMQ.Demo.LazyWorker
{
    public class Program
    {
        private static readonly string QUEUE_NAME = "task_queue";
        public static void Main(string[] args)
        {
            if (args.Length <= 0) throw new ArgumentException("Should give a worker name");
            else Console.WriteLine($"Lazy Worker - {args[0]} waiting messages");

            var channel = RabbitUtils.CreateChannel();
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) => {
                var message = Encoding.UTF8.GetString(e.Body.ToArray());
                Console.WriteLine($"Received: {message}");
            };

            channel.BasicConsume(QUEUE_NAME, true, consumer);
            Console.ReadKey();
        }
    }
}