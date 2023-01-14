using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FarmasiCase.Models;
using FarmasiCase.Services;

namespace FarmasiCase.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService UserService)
        {
            _userService = UserService;
        }
        
        [ProducesResponseType(typeof(User), 200)]
        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateModel model)
        {
            var user = _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [ProducesResponseType(typeof(User), 200)]
        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult Register([FromBody] AuthenticateModel model)
        {
            var user = _userService.Register(model.Username, model.Password);//servisten eşitliği kontrol ettigini anlıyoruz
            if (user == null)
            {
                return BadRequest("Username already exist");

            }
            else
            {
                return Ok(user);
            }

        }

    }
}