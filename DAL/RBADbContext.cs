using BOL;
using DAL.ExtensionMethods;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class RBADbContext: IdentityDbContext
    {
        public RBADbContext(DbContextOptions<RBADbContext> options): base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Add Seeding in Extension Methods
            modelBuilder.Seed();
        }

        public DbSet<ApplicationUsers> ApplicationUsers { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Items> Items { get; set; }
        public DbSet<OrderBill> OrderBill { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<Restaurant> Restaurant { get; set; }
    }
}
