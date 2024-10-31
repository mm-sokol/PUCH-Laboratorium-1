// using Microsoft.AspNetCore.Builder;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.EntityFrameworkCore;
// using SimpleApp.Models; // Adjust the namespace according to your project structure

// public class Startup
// {
//     public IConfiguration Configuration { get; }

//     public Startup(IConfiguration configuration)
//     {
//         Configuration = configuration;
//     }

//     public void ConfigureServices(IServiceCollection services)
//     {
//         // Register AppDbContext with the dependency injection container
//         services.AddDbContext<AppDbContext>(options =>
//             options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

//         // Add other services (e.g., MVC)
//         services.AddControllersWithViews();
//     }

//     public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//     {
//         if (env.IsDevelopment())
//         {
//             app.UseDeveloperExceptionPage();
//         }
//         else
//         {
//             app.UseExceptionHandler("/Home/Error"); // Adjust this according to your error handling
//             app.UseHsts();
//         }

//         app.UseHttpsRedirection();
//         app.UseStaticFiles();

//         app.UseRouting();

//         app.UseAuthorization();

//         app.MapControllerRoute(
//             name: "default",
//             pattern: "{controller=Home}/{action=Index}/{id?}");
//     }
// }
