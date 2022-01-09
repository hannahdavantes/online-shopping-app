using HannahDavantes_FinalProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HannahDavantes_FinalProject.Data.Utilities {
    public class DbContextUtility : IdentityDbContext<User> {
        public DbContextUtility(DbContextOptions<DbContextUtility> options) : base(options) {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<BasketProduct> BasketProducts { get; set; }
    }
}
