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
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using MainAPI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using System.Net.Http;
using MainAPI.Services;
using System.Net;
using System.Net.Http.Headers;

namespace Tests
{
    public class AuthorizationsTests
    {
        private TestServer server;
        private HttpClient client;
        public AuthorizationsTests()
        {
            var builder = WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>();


            server = new TestServer(builder);
            client = server.CreateClient();
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type","application/json");
        }
        [Fact]
        public async void UserRecievesATokenOnValidCredentials()
        {
            HttpStatusCode expectedCode = HttpStatusCode.OK;
            HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Post, "/api/user/login");
            msg.Content = new StringContent("{\"UserName\":\"qwerty\", \"Password\":\"55555\" }", Encoding.UTF8, "application/json");

            var jsonresult = await client.SendAsync(msg);

            Assert.Equal(expectedCode, jsonresult.StatusCode);
            Assert.NotNull(await jsonresult.Content.ReadAsStringAsync());
        }
        [Fact]
        public async void UserIsRedirectedToErrorPageOnInvalidCredentials()
        {
            HttpStatusCode expectedCode = HttpStatusCode.BadRequest;
            HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Post, "/api/user/login");
            msg.Content = new StringContent("{\"UserName\":\"qwerty\", \"Password\":\"wrongpassword\" }", Encoding.UTF8, "application/json");

            var jsonresult = await client.SendAsync(msg);

            Assert.Equal(expectedCode, jsonresult.StatusCode);
        }

        [Fact]
        public async void UserIsRedirectedToErrorOnAccessingSettings()
        {
            HttpStatusCode expectedCode = HttpStatusCode.Unauthorized;

            var result = await client.GetAsync("/api/user/settings");

            Assert.Equal(expectedCode, result.StatusCode);
        }

        [Fact]
        public async void LoggedInUserIsRedirectedToSettingsOnAccessingSettings()
        {
            HttpStatusCode expectedCode = HttpStatusCode.OK;

            HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Post, "/api/user/login");
            msg.Content = new StringContent("{\"UserName\":\"qwerty\", \"Password\":\"55555\" }", Encoding.UTF8, "application/json");
            string token = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(await (await client.SendAsync(msg)).Content.ReadAsStringAsync());

            msg = new HttpRequestMessage(HttpMethod.Get, "/api/user/settings");
            msg.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.SendAsync(msg);

            Assert.Equal(expectedCode, result.StatusCode);
        }

    }
}
