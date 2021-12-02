using RabbitMQ.Client;

namespace RabbitMQ.Demo.Common
{
    public class RabbitUtils
    {
        public static IModel CreateChannel()
        {
            var factory = new ConnectionFactory() { HostName = "localhost", UserName = "admin", Password = "admin" };
            var conn = factory.CreateConnection();
            return conn.CreateModel();
        }
    }
}