using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Catalog.Models
{
    public class TodoContext : DbContext
    {
        public DbSet<Pattern> db_patterns { get; set; }
        public DbSet<Category> db_categories { get; set; }

        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //один ко многим
            modelBuilder.Entity<Category>()
            .HasOne(p => p.patterns)
            .WithMany(c => c.categories)
            .HasForeignKey(i => i.PatternId);
        }

        public string PostCategory(Category category)
        {
            var list_p = db_patterns;
            foreach (var pattern in list_p)
            {
                if (pattern.Id == category.PatternId)
                {
                    pattern.categories.Add(category);
                    SaveChanges();
                    return "Category added successfully.";
                }
            }
            return "No pattern.";
        }

        public IEnumerable<Pattern> GetPatterns()
        {
            return db_patterns.Include(c => c.categories).ThenInclude(p => p.patterns).ToList();
        }

        public IEnumerable<Category> GetCategories()
        {
           return db_categories.Include(p => p.patterns).ThenInclude(c => c.categories).ToList();
        }
        public IEnumerable<Pattern> GetPatternByCategory(string name_category)
        {
            return GetCategories().Where(c => c.NameCategory == name_category).Select(p => p.patterns);
        }

        public IEnumerable<int> GetIdPatternByCategory(string name_category)
        {
            var i = db_categories.Where(c => c.NameCategory == name_category).Select(p => p.PatternId);
            return i;
        }
        public IEnumerable<Pattern> GetPatternByLevel(string name_level)
        {
            var list_p = db_patterns.Where(p => p.NameLevel == name_level);
            return list_p.Include(c => c.categories).ThenInclude(p => p.patterns).ToList();
        }
        public void ChangePrice(int id_pattern, int price)
        {
            var p = db_patterns.FirstOrDefault(id => id.Id == id_pattern);
            if (p != null)
            {
                p.Price = price;
                SaveChanges();
            }
        }
    }
}
