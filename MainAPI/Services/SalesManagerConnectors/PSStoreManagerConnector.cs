using MainAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainAPI.Services
{
    public class PSStoreManagerConnector : SalesManagerConnector
    {
        public PSStoreManagerConnector() //:base(new Uri("psstoreservice.com/api/getsales"))
        {

        }

        public override async Task<IEnumerable<Sale>> GetSales()
        {
            //return await serviceConnection.Request();
            return new List<Sale>()
            {
                new Sale(){Game = new Game(){Id=1,Name="Terraria"}, NormalPrice=1500,CurrentPrice=1000,Platform="PSStore",PlatformSpecificID="21"},
                new Sale(){Game = new Game(){Id=2,Name="Hollow Knight"}, NormalPrice=1750,CurrentPrice=1250,Platform="PSStore",PlatformSpecificID="22"},
                new Sale(){Game = new Game(){Id=3,Name="Steins;Gate"}, NormalPrice=3500,CurrentPrice=1500,Platform="PSStore",PlatformSpecificID="23"}
            };
        }
    }
}
