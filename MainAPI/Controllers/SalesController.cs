using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MainAPI.Controllers
{
    [Route("sales")]
    public class SalesController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;

        public SalesController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        public IActionResult NintendoSales()
        {
            return Redirect("/sales/nintendo");
        }
        public IActionResult SteamSales()
        {
            return Redirect("/sales/steam");
        }
        public IActionResult PSStoreSales()
        {
            return Redirect("/sales/ps");
        }


        private async Task<bool> IsRegistered()
        {
            var claims = User.Claims;
            var user = await userManager.FindByEmailAsync(User.Claims.Where(c => c.Type == ClaimTypes.Email).FirstOrDefault().ToString());
            return user != null;
        }

    }
}
