using MainAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainAPI.Services
{
    public class NintendoManagerConnector : SalesManagerConnector
    {
        public NintendoManagerConnector() //: base(new Uri("nintendomanager.com/api/getSales"))
        {
        }

        public override async Task<IEnumerable<Sale>> GetSales()
        {
            //return await serviceConnection.Request();
            return new List<Sale>()
            {
                new Sale(){Game = new Game(){Id=1,Name="Terraria"}, NormalPrice=1500,CurrentPrice=1000,Platform="NintendoStore",PlatformSpecificID="21"},
                new Sale(){Game = new Game(){Id=2,Name="Hollow Knight"}, NormalPrice=1750,CurrentPrice=1250,Platform="NintendoStore",PlatformSpecificID="122"},
                new Sale(){Game = new Game(){Id=3,Name="Steins;Gate"}, NormalPrice=3500,CurrentPrice=1500,Platform="NintendoStore",PlatformSpecificID="123"}
            };
        }
    }
}
