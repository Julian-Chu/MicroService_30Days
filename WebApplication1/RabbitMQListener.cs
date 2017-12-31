using APIGateway.Controllers;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace APIGateway
{
    public static class RabbitMQListener
    {
        private static IConnection connection;
        private static IModel channel;

        public static void Start() {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            channel.QueueDeclare(
                    queue: "registry",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                ServiceRegistryController src = new ServiceRegistryController();
                src.Post(message);
            };

            channel.BasicConsume(
                queue: "registry",
                autoAck: true,
                consumer: consumer);
        }

        public static void Stop() {
            channel.Close(200, "Bye");
            connection.Close();
        }
    }
}
