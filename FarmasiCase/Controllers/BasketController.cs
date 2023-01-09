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



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FarmasiCase.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly BasketService basketService;
        private readonly UserService userService;
        private readonly ProductService productService;
        public BasketController(BasketService service,UserService kullaniciServisi,ProductService urunServisi)
        {
            basketService = service;
            userService = kullaniciServisi;
            productService = urunServisi;
        }
        // GET: api/<BasketController>
        [HttpGet]
        public async Task<List<Product>> Get()
        {
            string kullaniciId = GetUserId();
            string[] basket = await basketService.GetBasket(kullaniciId);
            List<Product> urunler = new List<Product>();
            for (int i = 0; i < basket.Length; i++)
            {
                urunler = urunler.Append<Product>(productService.GetById(basket[i])).ToList();

            }
            return urunler;
        }

        // POST api/<BasketController>
        [HttpPost]
        public async Task<IActionResult> AddBasket(string productId)
        {
            Product product = productService.GetById(productId);
            if (product== null)
            {
                return BadRequest("ürün bulunamadı");
            }
            string kullaniciId=GetUserId();
            string[] basket = await basketService.GetBasket(kullaniciId);
            await basketService.SetBasket(kullaniciId, basket.Append(productId).ToArray());
            return Ok();

        }

        [HttpDelete]
        public void Temizle()
        {


        }

        private string GetUserId()
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
            var username = credentials[0];
            var password = credentials[1];
            User user = userService.Authenticate(username, password);
            return user.Id;
        }

     

    }
}
