using GroceryStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStore.Data
{
    public static class DbInitializer
    {
        public static void Initialize(GroceryStoreContext context)
        {
            if (!context.Admin.Any())
            {
                var admin = new Admin
                {
                    userName = "admin",
                    password = "admin"
                };
                context.Admin.Add(admin);
                context.SaveChanges();
            }
        }
    }
}
