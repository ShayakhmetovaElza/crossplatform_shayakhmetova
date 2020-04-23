using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Models
{
    public class Category
    {
        static int maxId = 0;
        public int Id { get; set; }
        public string NameCategory { get; set; }
        public Category(string category)
        {
            this.Id = maxId;
            maxId++;
            this.NameCategory = category;
        }
    }
}
