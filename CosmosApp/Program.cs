using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;


public class Program
{
        private static string? EndpointUri;
        private static string? PrimaryKey;
        private CosmosClient? cosmosClient;
        private Database? database;
        private Container? container;
        private string? databaseName;
        private string? containerName;
        private string? partitionKey;

    private void CreateCosmosClient(IConfiguration configuration) {
        EndpointUri = configuration["CosmosDb:AccountEndpoint"];
        PrimaryKey = configuration["CosmosDb:AccountKey"];
        databaseName = configuration["CosmosDb:DatabaseName"];
        containerName = configuration["CosmosDb:ContainerName"];
        partitionKey = configuration["CosmosDb:PartitionKey"];
        this.cosmosClient = new CosmosClient(configuration["CosmosDb:ConnectionString"]);
    }

    private async Task CreateDatabase() {
        this.database = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(databaseName);
        if (this.database == null)
        {
            throw new InvalidOperationException("Failed to create or retrieve the database.");
        }        
        Console.WriteLine($"Database: {this.databaseName} created\n");
    }

    private async Task CreateContainer() {
        this.container = await this.database.CreateContainerIfNotExistsAsync(this.containerName, this.partitionKey);
        if (this.container == null)
        {
            throw new InvalidOperationException("Failed to create or retrieve the container.");
        }        
        Console.WriteLine($"Container: {this.containerName} created");
    }

    public async Task<WebApplication> Setup(string[] args) {
        var builder = WebApplication.CreateBuilder(args);   
        builder.Configuration["ASPNETCORE_ENVIRONMENT"] = "Development";
        var config = builder.Configuration;

        this.CreateCosmosClient(config);
        await this.CreateDatabase();
        await this.CreateContainer();

        // builder.Services.AddHttpsRedirection(options =>
        // {
        //     options.HttpsPort = 5258;
        // });

        builder.WebHost.ConfigureKestrel(options =>
        {
            options.ListenLocalhost(7206, listenOptions => listenOptions.UseHttps());
        });

        builder.Services.AddControllersWithViews();
        var app = builder.Build();

        return app;
    }

    public static async Task Main(string[] args)
    {    
        try {
            Program p = new Program();
            var app = await p.Setup(args);

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseForwardedHeaders();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllers();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
            );

            app.MapControllerRoute(
                name: "api",
                pattern: "api/{controller}/{action}/{id?}");

            

            app.Run();
        }
        catch (CosmosException ce) {
            Exception baseException = ce.GetBaseException();
            Console.WriteLine($"{ce.StatusCode} error occured: {ce}");
        }
        catch (Exception e) {
            Console.WriteLine($"Error: {e}");
        }

    }
}
