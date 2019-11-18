using MainAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainAPI.Services
{
    public class SalesServiceConnection
    {
        Uri address;
        public SalesServiceConnection(Uri uri)
        {
            address = uri;
        }

        public async Task<IEnumerable<Sale>> Request()
        {
            throw new NotImplementedException();
        }
    }
}
