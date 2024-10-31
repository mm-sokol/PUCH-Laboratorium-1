using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using SimpleApp.Models;
class Program
{
    static async Task Main(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlServer("Server=tcp:puch.database.windows.net,1433;Initial Catalog=puch_db;Persist Security Info=False;User ID=puchlab;Password=puszek4ALL2006!!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        using (var context = new AppDbContext(optionsBuilder.Options))
        {
            var products = await context.Products.ToListAsync();

            Console.WriteLine("Products:");
            Console.WriteLine("ID\tColor\tStandard Cost\tModified Date");
            foreach (var product in products)
            {
                Console.WriteLine($"{product.ProductID}\t{product.Color}\t{product.StandardCost:C}\t{product.ModifiedDate:yyyy-MM-dd}");
            }
        }
    }
}



// public class Program
// {
//     public static void Main(string[] args)
//     {
//         CreateHostBuilder(args).Build().Run();
//     }

//     public static IHostBuilder CreateHostBuilder(string[] args)
//         => Host.CreateDefaultBuilder(args)
//             .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
// }



// class Program
// {


//     static async Task Main(string[] args)
//     {
//         var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
//         optionsBuilder.UseSqlServer("Server=tcp:puch.database.windows.net,1433;Initial Catalog=puch_db;Persist Security Info=False;User ID=puchlab;Password=puszek4ALL2006!!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

//         using (var context = new AppDbContext(optionsBuilder.Options))
//         {
//             var products = await context.Products.ToListAsync();

//             foreach (var product in products)
//             {
//                 Console.WriteLine($"ID: {product.ProductID}, Color: {product.Color}, Price: {product.StandardCost:C} Modified:{product.ModifiedDate:DD-MM-YYY}");
//             }
//         }
//     }
// }
