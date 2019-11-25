using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace MainAPI.Model
{
    public class Sale: IEquatable<Sale>
    {
        public Game Game { get; set; }
        public string Platform { get; set; }
        public string PlatformSpecificID {get; set; }
        public decimal NormalPrice { get; set; }
        public decimal CurrentPrice { get; set; }

        public bool Equals([AllowNull] Sale other)
        {
            return Platform == other.Platform && PlatformSpecificID == other.PlatformSpecificID
                && NormalPrice == other.NormalPrice && CurrentPrice == other.CurrentPrice
                && Game.Equals(other.Game);
        }
    }
}
