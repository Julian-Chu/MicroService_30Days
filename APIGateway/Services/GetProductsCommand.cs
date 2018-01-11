using Steeltoe.CircuitBreaker.Hystrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGateway.Services
{
    public class GetProductsCommand:HystrixCommand<string[]>
    {
        private readonly HystrixCommandOptions options;
        private readonly IProductService productService;

        public GetProductsCommand(HystrixCommandOptions options, IProductService productService) : base(options) {
            this.options = options;
            this.productService = productService;
            IsFallbackUserDefined = true;
        }

        public string[] GetProducts() {
            return Execute();
        }

        protected override string[] Run() {

            return productService.GetValues();
        }

        protected override string[] RunFallback() {
            return new string[] { "Error", "Error" };
        }
    }
}
