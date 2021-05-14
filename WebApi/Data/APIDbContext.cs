using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class APIDbContext:DbContext
    {
        public APIDbContext(DbContextOptions<APIDbContext> options) : base(options)
        {
          

        }
        public DbSet<Items> Items { get; set; }
    }
}
