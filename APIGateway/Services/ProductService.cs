using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
        private const string ProductServiceAPI_URL = "http://ProductService/api/values";
        private const string LocalURL = "http://localhost:1234/api/values/5";
        private const string FortuneURL = "http://fortuneService/api/fortunes/random";
        public ProductService(IDiscoveryClient client, ILoggerFactory logFactory) {
            _handler = new DiscoveryHttpClientHandler(client,
                logFactory.CreateLogger<DiscoveryHttpClientHandler>());
            _logger = logFactory.CreateLogger<ProductService>();            
        }

        public string GetValue(int id) {
            var client = new HttpClient(_handler, false);
            var result = client.GetStringAsync($"{ProductServiceAPI_URL}/{id}").Result;
            _logger.LogInformation("Get string:{0}", result);
            
            return result;
        }

        public string[] GetValues() {
            var client = new HttpClient(_handler, false);
            var response = client.GetAsync(ProductServiceAPI_URL).Result;
            var responseBody = response.Content.ReadAsStringAsync().Result;
            _logger.LogInformation("Get string:{0}", responseBody);
            var result = JsonConvert.DeserializeObject<List<string>>(responseBody);

            return result.ToArray();
        }
    }
}
