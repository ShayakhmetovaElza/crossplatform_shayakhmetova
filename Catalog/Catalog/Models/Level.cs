using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Models
{
    public class Level
    {
        static int maxId = 0;
        public int Id { get; set; }
        public string NameLevel { get; set; }

        public Level(string level)
        {
            this.Id = maxId;
            maxId++;
            this.NameLevel = level;
        }
    }
}
