using BLL;
using DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
            services.AddControllersWithViews();

            #region DAL Service
            services.AddTransient<IApplicationUsersDb, ApplicationUsersDb>();
            services.AddTransient<ICartDb, CartDb>();
            services.AddTransient<IItemsDb, ItemsDb>();
            #endregion

            #region BLL Service
            services.AddTransient<IApplicationUsersBs, ApplicationUsersBs>();
            services.AddTransient<ICartBs, CartBs>();
            services.AddTransient<IItemsBs, ItemsBs>();
            #endregion

            services.AddDbContext<RBADbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("dbx")));
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

            //app.UseAuthorization();
            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
