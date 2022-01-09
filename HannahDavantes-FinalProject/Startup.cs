using HannahDavantes_FinalProject.Data.Services;
using HannahDavantes_FinalProject.Data.Utilities;
using HannahDavantes_FinalProject.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HannahDavantes_FinalProject {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            //DBContext Configuration - translator between Models and Database
            //DefaultConnectionString is found in appsettings.json 
            services.AddDbContext<DbContextUtility>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString")));

            //Add Services
            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<IOrdersService, OrdersService>();

            //Services for Adding to Basket
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(b => Basket.GetBasket(b));
            services.AddSession();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //Configuration for using Http Sessions
            app.UseRouting();
            app.UseSession();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            DbInitializer.LoadInitialData(app);
        }
    }
}
