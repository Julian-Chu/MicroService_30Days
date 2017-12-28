using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using APIGateway.Models;

namespace APIGateway.Controllers
{
    [Produces("application/json")]
    [Route("api/ServiceRegistry")]
    public class ServiceRegistryController : Controller
    {
        static List<MicroService> serviceList = new List<MicroService>() {
            new MicroService() { Name="API Gateway", Location="http://localhost:2324" }
        };
        // GET: api/ServiceRegistry
        [HttpGet]
        public IEnumerable<MicroService> Get()
        {
            return serviceList;
        }

        // POST: api/ServiceRegistry
        [HttpPost]
        public void Post([FromBody]MicroService service)
        {
            serviceList.Add(service);
        }

        // DELETE: api/ServiceRegistry/{serviceName}
        [HttpDelete("{serviceName}")]
        public void Delete(string serviceName)
        {
            serviceList.Remove(serviceList.First(s => s.Name == serviceName));
        }
    }
}
