using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Demo.Common;
using System.Text;

namespace RabbitMQ.Demo.LazyAck
{
    public class Program
    {
        private static readonly string QUEUE_NAME = "task_queue";
        public static void Main(string[] args)
        {
            Console.WriteLine("Ack-Consumer is waiting messages");

            var channel = RabbitUtils.CreateChannel();
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) => {
                var message = Encoding.UTF8.GetString(e.Body.ToArray());
                Thread.Sleep(10000);
                Console.WriteLine($"Received: {message}");
                channel.BasicAck(e.DeliveryTag, false);
            };

            channel.BasicConsume(QUEUE_NAME, false, consumer);
            Console.ReadKey();
        }
    }
}