using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainAPI.Options
{
    public class JwtOptions
    {
        public const string ISSUER = "AdvDictionaryServer";
        public const string AUDIENCE = "http://0.0.0.0:5000/";
        const string Secret = "super secure mega key for this weird encryption with some spaces";
        public const int LIFETIME = 300;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));
        }
    }
}
