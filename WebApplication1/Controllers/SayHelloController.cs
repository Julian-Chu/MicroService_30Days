using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;

namespace APIGateway.Controllers
{
    [Produces("application/json")]
    [Route("api/SayHello")]
    public class SayHelloController : Controller
    {
        // GET: api/SayHello/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/SayHello
        [HttpPost]
        public string Post([FromQuery]string name)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection()) {
                using (var channel = connection.CreateModel()) {
                    channel.QueueDeclare(
                        queue: "hello",
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                        );

                    string message = name;
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: "hello", basicProperties: null,
                        body: body);
                }
            }

            return $"Hello,{name}";
        }
        

    }
}
