using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Models
{
    public class TodoContext : DbContext
    {
        public DbSet<Pattern> patterns { get; set; }
        public DbSet<Category> categories { get; set; }

        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        { }

    }
}
