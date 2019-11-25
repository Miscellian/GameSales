using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MainAPI.Services;

namespace MainAPI.Controllers
{
    [Route("/api/sales")]
    public class SalesController : Controller
    {
        private readonly SteamManagerConnector steamManager;
        private readonly NintendoManagerConnector nintendoManager;
        private readonly PSStoreManagerConnector psStoreManager;

        public SalesController(SteamManagerConnector steamManager, NintendoManagerConnector nintendoManager, PSStoreManagerConnector psStoreManager)
        {
            this.steamManager = steamManager;
            this.nintendoManager = nintendoManager;
            this.psStoreManager = psStoreManager;
        }

        [HttpGet("nintendo")]
        public async Task<JsonResult> NintendoSales()
        {
            return new JsonResult(await nintendoManager.GetSales());
        }
        [HttpGet("steam")]
        public async Task<JsonResult> SteamSales()
        {
            return new JsonResult(await steamManager.GetSales());
        }
        [HttpGet("psstore")]
        public async Task<JsonResult> PSStoreSales()
        {
            return new JsonResult(await psStoreManager.GetSales());
        }
    }
}
