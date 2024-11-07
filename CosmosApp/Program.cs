using Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;


public class CosmosDBService {
    private readonly string EndpointUri;

    private readonly string PrimaryKey;

    private readonly string databaseId;

    private readonly string containerId;

    private CosmosClient cosmosClient;

    private Database database;

    private Container container;


    public CosmosDBService(IConfiguration configuration) 
    {
        EndpointUri = configuration["CosmosDb:AccountEndpoint"];
        PrimaryKey = configuration["CosmosDb:AccountKey"];
        databaseName = configuration["CosmosDb:DatabaseName"];
        containerName = configuration["CosmosDb:ConatinerName"];
    }

}

public class Program
{

    public static void Main(string[] args)
    {    
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
