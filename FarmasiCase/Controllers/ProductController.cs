using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmasiCase.Services;
using FarmasiCase.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FarmasiCase.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService productService;
        public ProductController(ProductService service)
        {
            productService = service;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public List<Product> Get()
        {
            return productService.Get();
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public Product Get(string id)
        {
            return productService.GetById(id);
        }

        // POST api/<ProductController>
        [HttpPost]
        public void Post(ProductRequest productRequest)
        {
            Product newProduct = new Product(productRequest.name,productRequest.price);
            productService.Create(newProduct);
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(string id, ProductRequest updatedData)
        {
            Product newProduct = new Product(updatedData.name, updatedData.price);
            productService.Update(id,newProduct);
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(string Id)
        {
            productService.Delete(Id);

        }
    }
}
