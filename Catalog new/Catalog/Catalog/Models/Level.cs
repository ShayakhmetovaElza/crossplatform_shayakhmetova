using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Models
{
    public class Level
    {        
        public string NameLevel { get; set; }

        public Level(string level)
        {
            this.NameLevel = level;
        }
    }
}
