using Microsoft.Extensions.Logging;
using Pivotal.Discovery.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace APIGateway.Services
{
    public class ProductService : IProductService
    {
        DiscoveryHttpClientHandler _handler;
        ILogger<ProductService> _logger;
        private const string ProductServiceAPI_URL = "http://ProductService/api/values/5";
        private const string LocalURL = "http://localhost:1234/api/values/5";
        private const string FortuneURL = "http://fortuneService/api/fortunes/random";
        public ProductService(IDiscoveryClient client, ILoggerFactory logFactory) {
            _handler = new DiscoveryHttpClientHandler(client,
                logFactory.CreateLogger<DiscoveryHttpClientHandler>());
            _logger = logFactory.CreateLogger<ProductService>();            
        }

        public string GetValue() {
            var client = new HttpClient(_handler, false);


            var result = client.GetStringAsync(ProductServiceAPI_URL).Result;
            _logger.LogInformation("Get string:{0}", result);
            
            return result;
        }
    }
}
