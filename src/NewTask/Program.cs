using RabbitMQ.Demo.Common;
using System.Text;
using RabbitMQ.Client;

namespace RabbitMQ.Demo.NewTask
{
    public class Program
    {
        private static readonly string QUEUE_NAME = "task_queue";
        public static void Main(string[] args)
        {
            var channel = RabbitUtils.CreateChannel();
            channel.QueueDeclare(QUEUE_NAME, false, false, false, null);

            string? message;
            while((message = Console.ReadLine()) != null)
            {
                channel.BasicPublish("", QUEUE_NAME, null, Encoding.UTF8.GetBytes(message));
            }
        }
    }
}