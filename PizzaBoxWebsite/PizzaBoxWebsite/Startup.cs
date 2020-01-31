using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Interfaces;
using PizzaBox.Domain.Models;
using PizzaBox.Storing.Repositories;

namespace PizzaBoxWebsite
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
            string connectionString = Configuration.GetConnectionString("PizzaBoxDb");

            services.AddDbContext<PizzaBoxDbContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddTransient<IUserRepository<User>, UserRepository>();
            services.AddTransient<IStoreRepository<Store>, StoreRepository>();
            services.AddTransient<IOrderRepository<Order>, OrderRepository>();
            services.AddScoped<ISizeRepository<Size>, SizeRepository>();
            services.AddScoped<ICrustTypeRepository<CrustType>, CrustTypeRepository>();
            services.AddScoped<IPizzasSoldRepository<PizzaSold>, PizzasSoldRepository>();

            services.AddControllersWithViews();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=SignIn}/{id?}");
                endpoints.MapControllerRoute(
                    name: "StoreLocations",
                    pattern: "{controller=Home}/{action=StoreLocations}");
            });
        }
    }
}
