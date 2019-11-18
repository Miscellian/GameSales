using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainAPI.Model
{
    public class Sale
    {
        public string Game { get; set; }
        public string Platform { get; set; }
        public string PlatformSpecificID {get; set; }
        public decimal NormalPrice { get; set; }
        public decimal CurrentPrice { get; set; }
    }
}
