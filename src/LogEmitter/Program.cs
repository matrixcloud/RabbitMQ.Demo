using RabbitMQ.Demo.Common;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMQ.Demo.LogEmitter
{
    public class Program
    {
        private const string ExchangeName = "direct_logs";

        public static void Main(string[] args)
        {
            var channel = RabbitUtils.CreateChannel();
            channel.ExchangeDeclare(ExchangeName, ExchangeType.Direct);
            var dict = new Dictionary<string, string>() { 
                { "info", "This is an info message" }, 
                { "warning", "This is a warning message" },
                { "debug", "This is a debug message" }, 
                { "error", "This is an error message" } 
            };

            foreach (var kv in dict)
            {
                channel.BasicPublish(ExchangeName, kv.Key, null, Encoding.UTF8.GetBytes(kv.Value));
            }
        }
    }
}