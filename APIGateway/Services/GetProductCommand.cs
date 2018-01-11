using System;
using Microsoft.Extensions.Logging;
using Steeltoe.CircuitBreaker.Hystrix;

namespace APIGateway.Services
{
    public class GetProductCommand : HystrixCommand<string>
    {
        private readonly IHystrixCommandOptions options;
        private readonly IProductService productService;
        private int id;
        public GetProductCommand(IHystrixCommandOptions options, IProductService productService) : base(options) {
            this.options = options;
            this.productService = productService;
            IsFallbackUserDefined = true;
        }

        public string GetProduct(int id) {
            this.id = id;
            return Execute();
        }

        protected override string Run() {

            return productService.GetValue(this.id);
        }

        protected override string RunFallback() {
            return "Error";
        }
    }
}