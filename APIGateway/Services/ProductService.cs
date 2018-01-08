using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Pivotal.Discovery.Client;
using System.Collections.Generic;
using System.Net.Http;

namespace APIGateway.Services
{
  public class ProductService : IProductService
    {
        DiscoveryHttpClientHandler _handler;
        private const string ProductServiceAPI_URL = "http://ProductService/api/values";
        public ProductService(IDiscoveryClient client, ILoggerFactory logFactory) {
            _handler = new DiscoveryHttpClientHandler(client);
        }

        public string GetValue(int id) {
            var client = new HttpClient(_handler, false);
            var result = client.GetStringAsync($"{ProductServiceAPI_URL}/{id}").Result;
            return result;
        }

        public string[] GetValues() {
            var client = new HttpClient(_handler, false);
            var response = client.GetAsync(ProductServiceAPI_URL).Result;
            var responseBody = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<List<string>>(responseBody);

            return result.ToArray();
        }
    }
}
