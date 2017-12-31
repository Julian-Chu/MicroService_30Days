using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace SayHelloService
{
    class Program
    {
        static void Main(string[] args) {
            RegistryInAPIGateway();
            SubscribeToAPIGateway();
        }

        private static void SubscribeToAPIGateway() {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel()) {
                channel.QueueDeclare(
                    queue: "hello",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($"Received: {message}");
                    Console.WriteLine($"Hello, {message}");
                };

                channel.BasicConsume(
                    queue: "hello",
                    autoAck: true,
                    consumer: consumer);

                Console.WriteLine("Pess any key to exit");
                Console.ReadLine();
            }
        }

        private static void RegistryInAPIGateway() {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection()) {
                using (var channel = connection.CreateModel()) {
                    channel.QueueDeclare(
                        queue: "registry",
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                        );

                    string message = "SayHelloService";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: "registry", basicProperties: null,
                        body: body);
                }
            }
        }
    }
}
