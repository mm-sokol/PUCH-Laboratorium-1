using Azure.Data.Tables;
using CrudApp.src.Models;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Headers;
using System;
using Azure;




var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Configuration["ASPNETCORE_HTTPS_PORT"] = "5050";

builder.Services.AddSingleton<TableClient>(sp =>
{
    var connectionString = configuration["AzureTableStorage:ConnectionString"];
    var tableName = configuration["AzureTableStorage:TableName"];
    var serviceClient = new TableServiceClient(connectionString);
    var tableClient = serviceClient.GetTableClient(tableName);


    tableClient.CreateIfNotExists();
    return tableClient;
});

builder.Logging.AddConsole();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

builder.Services.Configure<HttpsRedirectionOptions>(options => {
    options.RedirectStatusCode = StatusCodes.Status308PermanentRedirect;
    options.HttpsPort = 5050;
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}

// Creating WeatherData endpoint
app.MapPost("api/weather/{partitionKey}/{rowKey}", async (string partitionKey, string rowKey, WeatherInsertData weather, TableClient tableClient) =>
{
    try
    {
        WeatherData data = new WeatherData{
            PartitionKey = partitionKey,
            RowKey = rowKey,
            Timestamp = DateTimeOffset.UtcNow,
            Temperature = weather.Temperature,
            Humidity = weather.Humidity,
            WindSpeed = weather.WindSpeed
        };

        await tableClient.AddEntityAsync(data);
        return Results.Created($"/api/weather/{data.PartitionKey}/{data.RowKey}", data);
    }
    catch (Exception ex)
    {
        return Results.Problem($"Error occurred while inserting data: {ex.Message}");
    }
})
.WithName("InsertWeatherData")
.WithOpenApi();


// Reading WeatherData endpoint
app.MapGet("/api/weather", async (TableClient tableClient) => {
    try {
        var weatherData = new List<WeatherData>();
        await foreach (WeatherData weather in tableClient.QueryAsync<WeatherData>())
        {
            Console.WriteLine($"{weather.PartitionKey}, {weather.RowKey}: temp.:{weather.Temperature}, humid.:{weather.Humidity} wind:{weather.WindSpeed}");
            weatherData.Add(weather);
        }


        return Results.Ok(weatherData);

    } catch (Exception ex) {
        return Results.Problem($"Error occured while fetching data: {ex.Message}");
    }
}).WithName("GetWeatherData").WithOpenApi();


// Updating WeatherData endpoint
app.MapPut("api/weather/{partitionKey}/{rowKey}", async (string partitionKey, string rowKey, WeatherUpdateData updateData, TableClient tableClient) => {

    try
    {
        var data = await tableClient.GetEntityAsync<WeatherData>(partitionKey, rowKey);
        if (data == null) {
            return Results.NotFound($"No data found with given PartitionKey: {partitionKey} and RowKey: {rowKey}");
        }

        Console.WriteLine($"{data.Value.PartitionKey}, {data.Value.RowKey} Temp.:{data.Value.Temperature} Humid.:{data.Value.Humidity} Wind:{data.Value.WindSpeed}");
        Console.WriteLine($"Update Data Temp.:{updateData.Temperature} Humid.:{updateData.Humidity} Wind:{updateData.WindSpeed}");

        if (updateData.Temperature.HasValue) {
            data.Value.Temperature = updateData.Temperature.Value;
        }
        // Console.WriteLine($"{updateData.Temperature.HasValue} -> {updateData.Temperature}");

        if (updateData.Humidity.HasValue) {
            data.Value.Humidity = updateData.Humidity.Value;
        }

        if (updateData.WindSpeed.HasValue) {
            data.Value.WindSpeed = updateData.WindSpeed.Value;
        }

        data.Value.Timestamp = DateTimeOffset.UtcNow;   

        await tableClient.UpdateEntityAsync(data.Value, data.Value.ETag, TableUpdateMode.Merge); 

        return Results.Ok(data.Value);
    }  
    catch (System.Exception ex)
    {
        return Results.Problem($"Error occured while updating data: {ex.Message}");
    }

}).WithName("UpdateWeatherData").WithOpenApi();


// Deleting WeatherData records
app.MapDelete("/api/weather/{partitionKey}/{rowKey}", async (string partitionKey, string rowKey, TableClient tableClient) =>
{
    try
    {
        await tableClient.DeleteEntityAsync(partitionKey, rowKey);
        return Results.NoContent();
    }
    catch (RequestFailedException ex) when (ex.Status == 404)
    {
        return Results.NotFound($"Weather data with PartitionKey '{partitionKey}' and RowKey '{rowKey}' was not found.");
    }
    catch (Exception ex)
    {
        return Results.Problem($"Error occurred while deleting data: {ex.Message}");
    }
})
.WithName("DeleteWeatherData")
.WithOpenApi();


app.MapControllers();

app.Run();
