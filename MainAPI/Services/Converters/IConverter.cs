using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainAPI.Services.Converters
{
    interface IConverter
    {
       Task<decimal> Convert(decimal amount, string currency);
    }
}
