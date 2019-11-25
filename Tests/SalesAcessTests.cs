using System;
using Xunit;
using MainAPI.Controllers;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using Microsoft.AspNetCore;
using MainAPI;
using Microsoft.AspNetCore.Hosting;
using MainAPI.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using MainAPI.Model;

namespace Tests
{
    public class SalesAcessTests
    {
        private TestServer server;
        private HttpClient client;

        public SalesAcessTests()
        {
            var builder = WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>();


            server = new TestServer(builder);
            client = server.CreateClient();
        }
        [Fact]
        public async void UserIsRedirectedToNintendoSales()
        {
            var expected = await new NintendoManagerConnector().GetSales();

            var jsonResult = await client.GetStringAsync("/api/sales/nintendo");

            Assert.Equal(expected, JsonConvert.DeserializeObject<IEnumerable<Sale>>(jsonResult));
        }

        [Fact]
        public async void UserIsRedirectedToSteamSales()
        {
            var expected = await new SteamManagerConnector().GetSales();

            var jsonResult = await client.GetStringAsync("/api/sales/steam");

            Assert.Equal(expected, JsonConvert.DeserializeObject<IEnumerable<Sale>>(jsonResult));
        }

        [Fact]
        public async void UserIsRedirectedToPSSToreSales()
        {
            var expected = await new PSStoreManagerConnector().GetSales();

            var jsonResult = await client.GetStringAsync("/api/sales/psstore");

            Assert.Equal(expected, JsonConvert.DeserializeObject<IEnumerable<Sale>>(jsonResult));
        }
    }
}
