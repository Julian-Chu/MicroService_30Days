using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIGateway.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIGateway
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        public ProductsController(GetProductCommand getProductCommand, GetProductsCommand getProductsCommand) {
            this.productServiceCommand = getProductCommand;
            this.getProductsCommand = getProductsCommand;
        }
        private readonly GetProductCommand productServiceCommand;
        private readonly GetProductsCommand getProductsCommand;

        // GET: api/products
        [HttpGet]
        public IEnumerable<string> Get() {
            return getProductsCommand.GetProducts();
        }

        // GET api/poducts/5
        [HttpGet("{id}")]
        public string Get(int id) {
            Console.WriteLine("Execute Get Product");
            return productServiceCommand.GetProduct(id);
        }


    }
}
