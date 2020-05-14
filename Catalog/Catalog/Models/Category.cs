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
        public int PatternId { get; set; }
        public Pattern patterns { get; set; }

    }
}
