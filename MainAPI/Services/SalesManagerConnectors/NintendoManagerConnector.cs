using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainAPI.Services
{
    public class NintendoManagerConnector : SalesManagerConnector
    {
        public NintendoManagerConnector() : 
            base(new Uri("NintendoManager/api/getSales"))
        {
        }
    }
}
