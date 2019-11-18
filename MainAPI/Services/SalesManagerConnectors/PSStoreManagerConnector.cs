using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainAPI.Services
{
    public class PSStoreManagerConnector : SalesManagerConnector
    {
        public PSStoreManagerConnector() :
            base(new Uri("PSStoreManager/api/getSales"))
        {

        }
    }
}
