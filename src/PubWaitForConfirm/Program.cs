using RabbitMQ.Demo.Common;
using RabbitMQ.Client;
using System.Diagnostics;
using System.Text;

namespace RabbitMQ.Demo.PubWaitForConfirm
{
    public class Program
    {
        private const int MessageCount = 100;

        public static void Main(string[] args)
        {

        }

        public static void PubWaitForConfirm()
        {
            using var channel = RabbitUtils.CreateChannel();
            var queueName = Guid.NewGuid().ToString();
            channel.QueueDeclare(queueName, false, false, true, null);
            channel.ConfirmSelect();

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            for (int i = 0; i < MessageCount; i++)
            {
                channel.BasicPublish("", queueName, null, Encoding.UTF8.GetBytes($"msg-{i}"));
                var r = channel.WaitForConfirms();
                Console.WriteLine($"Message send status: {r}");
            }
            stopWatch.Stop();
            Console.WriteLine($"Cost of PubWaitForConfirm: {stopWatch.ElapsedMilliseconds}");
        }
    }
}