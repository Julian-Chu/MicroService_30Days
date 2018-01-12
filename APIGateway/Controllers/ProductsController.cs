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
    private readonly GetProductCommand getProductCommand;
    private readonly GetProductsCommand getProductsCommand;

    // GET: api/values
    public ProductsController(GetProductCommand getProductCommand, GetProductsCommand getProductsCommand) {
      this.getProductCommand = getProductCommand;
      this.getProductsCommand = getProductsCommand;
    }

    [HttpGet]
    public IEnumerable<string> Get()
    {

      return getProductsCommand.GetProducts();
    }

    //[HttpGet]
    //public Task<string> Get() {

    //    return _productService.GetValuesArray();
    //}

    // GET api/values/5
    [HttpGet("{id}")]
        public string Get(int id) {
            Console.WriteLine("Execute Get Product");
            return getProductCommand.GetProduct(id);
        }


    }
}
