using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kitchen.Model
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options)
          : base(options)
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Line> Lines { get; set; }
        public DbSet<AreaKitchen> Kitchens { get; set; }

    }
}
