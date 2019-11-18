using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainAPI.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MainAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LogInDTO logIn)
        {
            var res = await signInManager.PasswordSignInAsync(logIn.Username, logIn.Password, true, false);
            if (res.Succeeded)
            {
                return Redirect("/home");
            } else
            {
                return Redirect("/error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Settings()
        {
            return User.Identity.IsAuthenticated
                ? Redirect("/settings")
                : Redirect("/login");
        }
    }
}