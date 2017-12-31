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
        public static void Main(string[] args) {

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build(); 
    }

}

