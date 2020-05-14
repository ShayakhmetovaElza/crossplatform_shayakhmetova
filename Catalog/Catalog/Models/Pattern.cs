using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Models
{
    public class Pattern
    {
         public int Id { get; set; }
        public string NamePattern { get; set; }
        public int Price { get; set; }
        public string NameLevel { get; set; }
        public List<Category> categories { get; set; }
        public Pattern() { categories = new List<Category>(); }
    }
}
