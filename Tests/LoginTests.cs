using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Xunit;
using MainAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using MainAPI.DTO;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Tests
{
    public class AuthorizationsTests
    {
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;
        public AuthorizationsTests()
        {
            userManager = new FakeUserManager(UserDBMock.GetContextMock().Users.FirstOrDefault());
            signInManager = new FakeSignInManager(UserDBMock.GetContextMock().Users.FirstOrDefault(), "testpwd");
        }
        [Fact]
        public async void UserIsRedirectedToHomeOnValidCredentials()
        {
            UserController controller = new UserController(userManager, signInManager);
            LogInDTO logIn = new LogInDTO() { Username = UserDBMock.GetContextMock().Users.FirstOrDefault().UserName, Password = "testpwd" };
            RedirectResult result = (RedirectResult)await controller.Login(logIn);

            Assert.Equal("/home", result.Url);
        }
        [Fact]
        public async void UserIsRedirectedToErrorPageOnInvalidCredentials()
        {
            UserController controller = new UserController(userManager, signInManager);
            LogInDTO logIn = new LogInDTO() { Username = UserDBMock.GetContextMock().Users.FirstOrDefault().UserName, Password = "wrongpassword" };
            RedirectResult result = (RedirectResult)await controller.Login(logIn);

            Assert.Equal("/error", result.Url);
        }

        [Fact]
        public async void UserIsRedirectedToErrorOnAccessingSettings()
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "test")
            };
            var identity = new ClaimsIdentity(claims);
            var claimsPrincipal = new ClaimsPrincipal(identity);
            var context = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = claimsPrincipal
                }
            };
            UserController controller = new UserController(userManager, signInManager);
            controller.ControllerContext = context;
            RedirectResult result = (RedirectResult)await controller.Settings();
            Assert.Equal("/login", result.Url);
        }

        [Fact]
        public async void LoggedInUserIsRedirectedToSettingsOnAccessingSettings()
        {

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "test")
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            var context = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = claimsPrincipal
                }
            };
            UserController controller = new UserController(userManager, signInManager);
            controller.ControllerContext = context;
            RedirectResult result = (RedirectResult)await controller.Settings();
            Assert.Equal("/settings", result.Url);
        }
       
    }
}
