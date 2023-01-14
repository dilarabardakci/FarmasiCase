using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmasiCase.Services;
using FarmasiCase.Models;
using Microsoft.AspNetCore.Authorization;

namespace FarmasiCase.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;
        public ProductController(ProductService ProductService)
        {
            _productService = ProductService; //readonly oldugu için bir kere set edilebilir bir daha initialize edilemez.
        }

        // GET: api/<ProductController>
        [HttpGet]
        public List<Product> Get()
        {
            return _productService.Get();
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public Product Get(string id)
        {
            return _productService.GetById(id);
        }

        // POST api/<ProductController>
        [HttpPost]
        public void Post(ProductRequest productRequest)
        {
            Product newProduct = new Product(productRequest.name,productRequest.price);
            _productService.Create(newProduct);
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(string id, ProductRequest updatedData)
        {
            Product newProduct = new Product(updatedData.name, updatedData.price);
            newProduct.Id = id;
            _productService.Update(id,newProduct);
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(string Id)
        {
            _productService.Delete(Id);

        }
    }
}
