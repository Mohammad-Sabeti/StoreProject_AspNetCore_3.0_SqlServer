using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyStore_Core3.DomainClasses;

namespace MyStore_Core3.DataLayer.Context
{

   public class MyStore_Core3DbContext:IdentityDbContext<IdentityUser>
   {
        public MyStore_Core3DbContext(DbContextOptions<MyStore_Core3DbContext> options):base(options)
        {

        }

       public DbSet<Product> Products { get; set; }
       public DbSet<ProductGroup> ProductGroups { get; set; }
       public DbSet<OrderApp> OrderApps { get; set; }


    }
}
