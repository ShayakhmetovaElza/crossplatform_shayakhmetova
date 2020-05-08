using System;
using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Models
{
    public class Category
    {
        public int Id { get; set; }
        static int maxId = 0;
        public string NameCategory { get; set; }

        public Category(string category)
        {
            this.Id = maxId;
            maxId++;
            this.NameCategory = category;
        }
        public Category() {}
    }
}
