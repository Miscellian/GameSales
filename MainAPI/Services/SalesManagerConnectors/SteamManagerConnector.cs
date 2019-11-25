using MainAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainAPI.Services
{
    public class SteamManagerConnector : SalesManagerConnector
    {
        public SteamManagerConnector() //: base(new Uri("steammanager.com/api/getsales"))
        {
        }

        public override async Task<IEnumerable<Sale>> GetSales()
        {
            //return await serviceConnection.Request();
            return new List<Sale>()
            {
                new Sale(){Game = new Game(){Id=1,Name="Terraria"}, NormalPrice=1000,CurrentPrice=500,Platform="Steam",PlatformSpecificID="1"},
                new Sale(){Game = new Game(){Id=2,Name="Hollow Knight"}, NormalPrice=750,CurrentPrice=250,Platform="Steam",PlatformSpecificID="2"},
                new Sale(){Game = new Game(){Id=3,Name="Steins;Gate"}, NormalPrice=1500,CurrentPrice=1000,Platform="Steam",PlatformSpecificID="3"}
            };
        }
    }
}
