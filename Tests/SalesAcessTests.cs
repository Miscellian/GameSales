using System;
using Xunit;
using MainAPI.Controllers;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Tests
{
    public class SalesAcessTests
    {
        private UserManager<IdentityUser> userManager;

        public SalesAcessTests()
        {
            userManager = new FakeUserManager(UserDBMock.GetContextMock().Users.FirstOrDefault());
        }
        [Fact]
        public void UserIsRedirectedToNintendoSales()
        {
            SalesController controller = new SalesController(userManager);
            RedirectResult result = (RedirectResult)controller.NintendoSales();

            Assert.Equal("/sales/nintendo", result.Url);
        }

        [Fact]
        public void UserIsRedirectedToSteamSales()
        {
            SalesController controller = new SalesController(userManager);
            RedirectResult result = (RedirectResult)controller.SteamSales();

            Assert.Equal("/sales/steam", result.Url);
        }

        [Fact]
        public void UserIsRedirectedToPSSToreSales()
        {
            SalesController controller = new SalesController(userManager);
            RedirectResult result = (RedirectResult)controller.PSStoreSales();

            Assert.Equal("/sales/ps", result.Url);
        }
    }
}
