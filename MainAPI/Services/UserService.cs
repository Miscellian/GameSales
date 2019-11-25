using MainAPI.Model;
using MainAPI.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MainAPI.Services
{
    public class UserService
    {
        private static List<User> users = new List<User>
        {
            new User {UserName="test", Password="12345"},
            new User {UserName="qwerty", Password="55555"}
        };
        public bool AuthenticateUser(string username, string password)
        {
            return users.Where(user => user.UserName == username && user.Password == password).Any();
        }

        public static JwtSecurityToken CreateToken(User user)
        {
            var claims = new List<Claim>(){
                new Claim(ClaimTypes.Name,user.UserName)
            };
            return new JwtSecurityToken(
                    issuer: JwtOptions.ISSUER,
                    audience: JwtOptions.AUDIENCE,
                    claims: claims,
                    notBefore: DateTime.Now,
                    expires: DateTime.Now.Add(TimeSpan.FromMinutes(JwtOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(JwtOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
        }
    }
}
