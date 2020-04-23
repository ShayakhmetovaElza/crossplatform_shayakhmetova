using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Models
{
    public class Pattern
    {
        static int maxId = 0;
        public long Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int IdLevel { get; set; }
        public int IdCategory { get; set; }

        public Pattern(string name, int price, int id_level, int id_category)
        {
            this.Id = maxId;
            maxId++;
            this.Name = name;
            this.Price = price;
            this.IdLevel = id_level;
            this.IdCategory = id_category;
        }
    }
}
