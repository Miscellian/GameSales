using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainAPI.Services
{
    public class SteamManagerConnector : SalesManagerConnector
    {
        public SteamManagerConnector() : 
            base(new Uri("SteamManager/api/getsales"))
        {
        }
    }
}
