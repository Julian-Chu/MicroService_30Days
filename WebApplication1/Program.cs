using System.Text;
using APIGateway.Controllers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace APIGateway
{
    public class Program
    {
        //static EventingBasicConsumer consumer;
        //static IConnection connection;
        //static IModel channel;

        public static void Main(string[] args) {
            //RegistrySubscriber();
            RabbitMQListener.Start();
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

        //private static void RegistrySubscriber() {
        //    var factory = new ConnectionFactory() { HostName = "localhost" };
        //    connection = factory.CreateConnection();
        //    channel = connection.CreateModel();
        //    channel.QueueDeclare(
        //        queue: "registry",
        //        durable: false,
        //        exclusive: false,
        //        autoDelete: false,
        //        arguments: null);

        //    consumer = new EventingBasicConsumer(channel);

        //    consumer.Received += (model, ea) =>
        //    {
        //        var body = ea.Body;
        //        var message = Encoding.UTF8.GetString(body);
        //        ServiceRegistryController src = new ServiceRegistryController();
        //        src.Post(message);

        //    };

        //    channel.BasicConsume(
        //        queue: "registry",
        //        autoAck: true,
        //        consumer: consumer);
        //}
    }

}

