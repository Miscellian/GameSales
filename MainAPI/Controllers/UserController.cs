using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using MainAPI.DTO;
using MainAPI.Model;
using MainAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MainAPI.Controllers
{
    [Route("/api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService userManager;

        public UserController(UserService userManager)
        {
            this.userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            Console.WriteLine(user.Password);
            Console.WriteLine(user.UserName);
            var res = userManager.AuthenticateUser(user.UserName, user.Password);
            if (res)
            {
                var response = (new JwtSecurityTokenHandler()).WriteToken(UserService.CreateToken(user));
                return new JsonResult(response);
            } else
            {
                return new BadRequestResult();
            }
        }

        [HttpGet("settings")]
        [Authorize(AuthenticationSchemes ="Bearer")]
        public async Task<IActionResult> Settings()
        {
            return new OkResult();
        }
    }
}