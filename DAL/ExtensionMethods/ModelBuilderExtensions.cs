using BOL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.ExtensionMethods
{
    
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {

            // any guid
            const string ADMIN_ROLE_ID = "a18be9c0-aa65-4af8-bd17-00bd9344e575";
            const string MANAGER_ROLE_ID = "a18bF1b0-aa65-4af8-bd17-00bd9344e575";
            const string CUSTOMER_ROLE_ID = "a18bF1b0-aa65-189f-bd17-00bd9344e575";
            const string MANAGER_ID = "USB3b1-aa65-189f-14pd-00bd9344e575";

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = ADMIN_ROLE_ID, Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole() { Id = MANAGER_ROLE_ID, Name = "Manager", NormalizedName = "MANAGER" },
                new IdentityRole() { Id = CUSTOMER_ROLE_ID, Name = "Customer", NormalizedName = "CUSTOMER" }
            );

            var hasher = new PasswordHasher<ApplicationUsers>();
            modelBuilder.Entity<ApplicationUsers>().HasData(
            
            new ApplicationUsers
            {
                Id = MANAGER_ID,
                UserName = "Manager",
                NormalizedUserName = "MANAGER",
                Email = "manager@gmail.com",
                NormalizedEmail = "manager@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Manager@123"),
                SecurityStamp = string.Empty
            }
            );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(

                new IdentityUserRole<string> { RoleId = MANAGER_ROLE_ID, UserId = MANAGER_ID }
             );
        }
    }
}
