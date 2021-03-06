using RabbitMQ.Demo.Common;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQ.Demo.InfoLogReceiver
{
    public class Program
    {
        private const string ExchangeName = "direct_logs";
        private const string QueueName = "console";

        public static void Main(string[] args)
        {
            var channel = RabbitUtils.CreateChannel();
            channel.ExchangeDeclare(ExchangeName, ExchangeType.Direct);
            channel.QueueDeclare(QueueName, false, false, false, null);
            channel.QueueBind(QueueName, ExchangeName, "info");
            channel.QueueBind(QueueName, ExchangeName, "warning");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) => {
                var message = Encoding.UTF8.GetString(e.Body.ToArray());
                Console.WriteLine($"Info-or-Warning-Received: {message}");
            };

            channel.BasicConsume(QueueName, true, consumer);
            Console.ReadKey();
        }
    }
}