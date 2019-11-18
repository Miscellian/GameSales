using MainAPI.DAO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    static class UserDBMock
    {
        private static UserDB InitializeMock()
        {
            var options = new DbContextOptionsBuilder<UserDB>()
                .UseInMemoryDatabase(databaseName: "Users")
                .Options;
            return new UserDB(options);
        }

        private static void FillMock(UserDB dbcontextMock)
        {
            dbcontextMock = InitializeMock();
            IdentityUser user = new IdentityUser("test");
            dbcontextMock.Users.Add(user);
            dbcontextMock.SaveChanges();
        }

        public static UserDB GetContextMock()
        {
            UserDB dbcontextMock = InitializeMock();
            FillMock(dbcontextMock);
            return dbcontextMock;
        }
    }
}
