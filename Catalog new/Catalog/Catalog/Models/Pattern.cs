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
        static int maxId = 0;
        [BindRequired] public string Name { get; set; }
        public int Price { get; set; }
        public string NameLevel { get; set; }
        public List<int> CategoryIds { get; set; }
        List<Level> levels = new List<Level>() 
        {   
            new Level("Easy"), 
            new Level("Average"), 
            new Level("Advanced") 
        };
        public Pattern() {}//this.CategoryIds = new List<int>(); }
        public Pattern(string name, int price, string name_level, List<int> c)
        {
            this.Id = maxId;
            maxId++;
            this.Name = name;
            this.Price = price;
            if (levels.FirstOrDefault(p => p.NameLevel == name_level) == null) this.NameLevel = null;
            else this.NameLevel = name_level;
            this.CategoryIds = c;
        }
    }
}
