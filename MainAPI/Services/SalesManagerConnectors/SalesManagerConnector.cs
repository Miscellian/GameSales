using MainAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainAPI.Services
{
    public abstract class SalesManagerConnector
    {
        SalesServiceConnection serviceConnection;
        public SalesManagerConnector(Uri uri)
        {
            serviceConnection = new SalesServiceConnection(uri);
        }
        public SalesManagerConnector()
        {
        }

        public abstract Task<IEnumerable<Sale>> GetSales();
    }
}
