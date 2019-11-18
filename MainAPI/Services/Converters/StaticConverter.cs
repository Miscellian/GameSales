using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainAPI.Services.Converters
{
    public class StaticConverter : IConverter
    {
        public Task<decimal> Convert(decimal amount, string currency)
        {
            throw new NotImplementedException();
        }
    }
}
