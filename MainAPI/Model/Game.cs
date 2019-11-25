using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace MainAPI.Model
{
    public class Game : IEquatable<Game>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Equals(Game other)
        {
            return Id == other.Id &&
                   Name == other.Name;
        }
    }
}
