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
    public class ValuesController : Controller
    {
        // GET: api/values
        public ValuesController(IProductService service) {
            _productService = service;
        }
        IProductService _productService;

        //[HttpGet]
        //public IEnumerable<string> Get()
        //{

        //    return _productService.GetValuesArray();
        //}

        //[HttpGet]
        //public Task<string> Get() {

        //    return _productService.GetValuesArray();
        //}

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id) {
            return _productService.GetValuesArray();
        }


    }
}
