using BLL;
using BOL;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserInterface
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region DAL Services
            services.AddTransient<ICartDb, CartDb>();
            services.AddTransient<IItemsDb, ItemsDb>();
            services.AddTransient<IRestaurantDb, RestaurantDb>();
            services.AddTransient<IApplicationUsersDb, ApplicationUsersDb>();
            services.AddTransient<IOrderBillDb, OrderBillDb>();
            services.AddTransient<IOrderDetailDb, OrderDetailDb>();
            #endregion
            #region BLL Services
            services.AddTransient<ICartBs, CartBs>();
            services.AddTransient<IItemsBs, ItemsBs>();
            services.AddTransient<IRestaurantBs, RestaurantBs>();
            services.AddTransient<IApplicationUsersBs, ApplicationUsersBs>();
            services.AddTransient<IOrderBillBs, OrderBillBs>();
            services.AddTransient<IOrderDetailBs, OrderDetailBs>();
            #endregion

            services.AddControllersWithViews();
            services.AddDbContext<RBADbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("dbx")));

            services.AddIdentity<ApplicationUsers, IdentityRole>()
                .AddEntityFrameworkStores<RBADbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options => options.LoginPath = "/Security/Login");

           var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
           services.AddControllersWithViews(x => x.Filters.Add(new AuthorizeFilter(policy)));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
