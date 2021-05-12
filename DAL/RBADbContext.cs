using BOL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class RBADbContext:DbContext
    {
        public RBADbContext(DbContextOptions<RBADbContext> options):base(options)
        {

        }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=DESKTOP-NTND64I\SQLEXPRESS;Database=RBADatabase; Trusted_Connection=True;");
        //}

        public DbSet<ApplicationUsers> ApplicationUsers { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Items> Items { get; set; }
        public DbSet<OrderBill> OrderBill { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<Restaurant> Restaurant { get; set; }
    }
}
