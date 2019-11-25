using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainAPI.Services.Converters
{
    public class StaticConverter : IConverter
    {
        private static Dictionary<string, Func<decimal, decimal>> converters = new Dictionary<string, Func<decimal, decimal>>()
        {
            { "UAH",(a) => a * 25 },
            { "EUR", (a) => a * 0.91m }
        };
        public Task<decimal> Convert(decimal amount, string currency)
        {
            throw new NotImplementedException();
        }
    }
}
