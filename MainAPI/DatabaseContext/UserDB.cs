using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MainAPI.DatabaseContext
{
    public class UserDB : IdentityDbContext<IdentityUser>
    {
        public UserDB(DbContextOptions contextOptions) : base (contextOptions)
        {

        }
    }
}
