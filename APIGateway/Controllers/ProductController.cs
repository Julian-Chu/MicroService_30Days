using System.Collections.Generic;
using APIGateway.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIGateway
{
  [Route("api/[controller]")]
    public class ProductController : Controller
    {
        // GET: api/values
        public ProductController(IProductService service) {
            _productService = service;
        }
        IProductService _productService;

        [HttpGet]
        public IEnumerable<string> Get() {

            return _productService.GetValues();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id) {
            return _productService.GetValue(id);
        }


    }
}
