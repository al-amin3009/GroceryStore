using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroceryStore.Models;

namespace GroceryStore.Data
{
    public class GroceryStoreContext : DbContext
    {
        public GroceryStoreContext(DbContextOptions<GroceryStoreContext> options)
            : base(options)
        {
        }

        public DbSet<Admin> Admin { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Products> Products { get; set; }

    }
}
