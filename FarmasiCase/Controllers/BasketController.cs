using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmasiCase.Models;
using FarmasiCase.Services;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http.Headers;
using System.Text;

namespace FarmasiCase.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly BasketService _basketService;
        private readonly UserService _userService;
        private readonly ProductService _productService;
        public BasketController(BasketService BasketService,UserService UserService,ProductService ProductService)
        {
            _basketService = BasketService;
            _userService = UserService;
            _productService = ProductService;
        }
        // GET: api/<BasketController>
        [HttpGet]
        public async Task<List<Product>> Get()
        {
            string userId = GetUserId();
            string[] basket = await _basketService.GetBasket(userId);
            List<Product> products = new List<Product>();
            for (int i = 0; i < basket.Length; i++)
            {
                products = products.Append<Product>(_productService.GetById(basket[i])).ToList();

            }
            return products;
        }

        // POST api/<BasketController>
        [HttpPost]
        public async Task<IActionResult> AddBasket(string productId)
        {
            Product product = _productService.GetById(productId);
            if (product== null)
            {
                return BadRequest("ürün bulunamadı");
            }
            string userId=GetUserId();
            string[] basket = await _basketService.GetBasket(userId);
            await _basketService.SetBasket(userId, basket.Append(productId).ToArray());
            return Ok();

        }

        [HttpDelete]
        public void ClearBasket()
        {
            _basketService.ClearBasket(GetUserId());
        }

        [ProducesResponseType(typeof(List<Product>), 200)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteItem(string id)
        {
            string UserId = GetUserId();
            string[] Basket = await _basketService.RemoveItem(UserId, id);
            if (Basket == null)
            {
                return NotFound("Item could not found");
            }
            else
            {
                List<Product> products = new List<Product>();
                for (int i = 0; i < Basket.Length; i++)
                {
                    products = products.Append<Product>(_productService.GetById(Basket[i])).ToList();
                }
                return Ok(products);
            }
        }

        private string GetUserId()
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
            var username = credentials[0];
            var password = credentials[1];
            User user = _userService.Authenticate(username, password);
            return user.Id;
        }
    }
}
