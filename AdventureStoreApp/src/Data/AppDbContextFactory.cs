using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace AdventureStoreApp.src.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args = null)
        {
            // Użyj konfiguracji z appsettings.json
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // Ścieżka do pliku konfiguracyjnego
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Ustala katalog roboczy
                .AddJsonFile("appsettings.json") // Dodaje plik konfiguracyjny
                .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("AzureSqlDb")); // Użyj connection stringa

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
