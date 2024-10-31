using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AdventureStoreApp.src.Data;
using AdventureStoreApp.src.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace AdventureStoreApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Dodaj DbContext z connection stringiem
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AzureSqlDb")));
            
            services.AddScoped<IProductService, ProductService>();

            services.AddControllersWithViews();
            // .AddRazorOptions(options =>
            // {
            //     options.ViewLocationFormats.Add("/src/Views/{1}/{0}.cshtml");
            //     options.ViewLocationFormats.Add("src/Views/{1}/{0}.cshtml");
            // });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                
                endpoints.MapControllerRoute(
                    name: "products",
                    pattern: "Products",
                    defaults: new { controller = "Products", action = "Index" }
                );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Products}/{action=Details}/{id?}");
            });

        }
    }
}
