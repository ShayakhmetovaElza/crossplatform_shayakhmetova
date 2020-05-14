using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Models
{
    public class Database
    {
        List<Pattern> list_patterns { get; set; }
        List<Category> list_categories { get; set; }

        public Database()
        {

            list_categories = new List<Category>
            {
                  new Category { NameCategory = "New", PatternId = 1 },
                  new Category { NameCategory = "New", PatternId = 5},
                  new Category { NameCategory ="Dresses", PatternId = 2 },
                  new Category { NameCategory ="Dresses", PatternId = 3 },
                  new Category { NameCategory ="Skirts", PatternId = 4 },
                  new Category { NameCategory ="Blouse", PatternId = 6 },
            };
            list_patterns = new List<Pattern>
                {
                   new Pattern{ NamePattern = "Dresses A",
                                Price = 150, NameLevel = "Easy" },
                   new Pattern{ NamePattern = "Dresses B",
                                Price = 200, NameLevel = "Average"},
                   new Pattern{ NamePattern = "Dresses C",
                                Price = 200, NameLevel = "Advanced"},
                   new Pattern{ NamePattern = "Skirts A",
                                Price = 100, NameLevel = "Easy" },
                   new Pattern{ NamePattern = "Skirts B",
                                Price = 50, NameLevel = "Average" },
                   new Pattern{ NamePattern = "Blouse A",
                                Price = 200, NameLevel = "Advanced" },
            };

        }
        public IEnumerable<Pattern> getpat()
        {
            return list_patterns;
        }
        public IEnumerable<Category> getcat()
        {
            return list_categories;
        }
    }
}
