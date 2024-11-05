using Azure.Data.Tables;
using CrudApp.src.Models;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;


var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Configuration["ASPNETCORE_HTTPS_PORT"] = "5050";

builder.Services.AddSingleton<TableClient>(sp =>
{
    var connectionString = configuration["TableStorage:ConnectionString"];
    var tableName = configuration["TableStorage:TableName"];
    var serviceClient = new TableServiceClient(connectionString);
    var tableClient = serviceClient.GetTableClient(tableName);


    tableClient.CreateIfNotExists();
    return tableClient;
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.Configure<HttpsRedirectionOptions>(options => {
    options.RedirectStatusCode = StatusCodes.Status308PermanentRedirect;
    options.HttpsPort = 5050;
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();


app.MapPost("/api/weather", async (WeatherData weather, TableClient tableClient) =>
{
    try
    {
        if (string.IsNullOrEmpty(weather.PartitionKey))
            weather.PartitionKey = "DefaultPartition";
        if (string.IsNullOrEmpty(weather.RowKey))
            weather.RowKey = Guid.NewGuid().ToString();

        await tableClient.AddEntityAsync(weather);
        return Results.Created($"/api/weather/{weather.RowKey}", weather);
    }
    catch (Exception ex)
    {
        return Results.Problem($"Error occurred while inserting data: {ex.Message}");
    }
})
.WithName("InsertWeatherData")
.WithOpenApi();



app.MapControllers();

app.Run();
