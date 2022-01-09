using HannahDavantes_FinalProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HannahDavantes_FinalProject.Data.Utilities {

    /// <summary>
    /// This class serves as the translator between the Models and Database tables
    /// </summary>
    public class DbContextUtility : IdentityDbContext<User> {
        public DbContextUtility(DbContextOptions<DbContextUtility> options) : base(options) {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
        }
        //These represents the tables from the database and this is where we can use LINQ queries to do CRUD operations
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<BasketProduct> BasketProducts { get; set; }
    }
}
