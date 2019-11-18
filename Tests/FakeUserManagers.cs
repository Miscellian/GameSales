using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class FakeSignInManager : SignInManager<IdentityUser>
    {
        IdentityUser User;
        string password;
        public FakeSignInManager(IdentityUser user, string password)
                : base(new Mock<FakeUserManager>(user).Object,
                     new Mock<IHttpContextAccessor>().Object,
                     new Mock<IUserClaimsPrincipalFactory<IdentityUser>>().Object,
                     new Mock<IOptions<IdentityOptions>>().Object,
                     new Mock<ILogger<SignInManager<IdentityUser>>>().Object,
                     new Mock<IAuthenticationSchemeProvider>().Object,
                     new Mock<IUserConfirmation<IdentityUser>>().Object)
        {
            User = user;
            this.password = password;
        }

        public override Task SignInAsync(IdentityUser user, bool persistence, string authenticationMethod)
        {
            return Task.FromResult(SignInResult.Success);
        }

        public override Task<SignInResult> PasswordSignInAsync(IdentityUser user, string password, bool persistence, bool lockoutOnFailure)
        {
            if (user == User && this.password == password)
                return Task.FromResult(SignInResult.Success);
            return Task.FromResult(SignInResult.Failed);
        }

        public override Task<SignInResult> PasswordSignInAsync(string username, string password, bool persistence, bool lockoutOnFailure)
        {
            if (username == User.UserName && this.password == password)
                return Task.FromResult(SignInResult.Success);
            return Task.FromResult(SignInResult.Failed);
        }

    }



    public class FakeUserManager : UserManager<IdentityUser>
    {
        IdentityUser user;
        public FakeUserManager(IdentityUser user)
            : base(new Mock<IUserStore<IdentityUser>>().Object,
              new Mock<IOptions<IdentityOptions>>().Object,
              new Mock<IPasswordHasher<IdentityUser>>().Object,
              new IUserValidator<IdentityUser>[0],
              new IPasswordValidator<IdentityUser>[0],
              new Mock<ILookupNormalizer>().Object,
              new Mock<IdentityErrorDescriber>().Object,
              new Mock<IServiceProvider>().Object,
              new Mock<ILogger<UserManager<IdentityUser>>>().Object)
        {
            this.user = user;
        }

        public override Task<IdentityResult> CreateAsync(IdentityUser user, string password)
        {
            return Task.FromResult(IdentityResult.Success);
        }

        public override Task<IdentityResult> AddToRoleAsync(IdentityUser user, string role)
        {
            return Task.FromResult(IdentityResult.Success);
        }

        public override Task<string> GenerateEmailConfirmationTokenAsync(IdentityUser user)
        {
            return Task.FromResult(Guid.NewGuid().ToString());
        }

        public override Task<IdentityUser> FindByEmailAsync(string email)
        {
            return Task.FromResult(user);
        }
    }
}
