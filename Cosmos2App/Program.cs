
using Microsoft.Azure.Cosmos;
using Cosmos2App.Services;
using Cosmos2App.Models;






var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<CosmosDbService>(s => {
    var conf = s.GetRequiredService<IConfiguration>();
    var cosmosConf = conf.GetSection("CosmosDb");

    var connParams = cosmosConf["ConnectionString"];
    var databaseName = cosmosConf["DatabaseName"];
    var containerName = cosmosConf["ContainerName"];

    if (connParams==null) {
        throw new ArgumentNullException("Missing 'ConnectionString'");
    }
    if (databaseName==null) {
        throw new ArgumentNullException("Missing 'DatabaseName'");
    }
    if (containerName==null) {
        throw new ArgumentNullException("Missing 'ContainerName'");
    }
    return new CosmosDbService(connParams, databaseName, containerName);

});

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapGet("/galaxies", async (CosmosDbService db) =>
{
    var galaxies = await db.GetGalaxiesAsync();
    return Results.Ok(galaxies);
})
.WithName("GetGalaxies")
.WithOpenApi();

app.MapPost("/galaxy" , async (Galaxy galaxy, CosmosDbService db) => {
    await db.CreateGalaxyAsync(galaxy);
    return Results.Created($"/galaxy/{galaxy.Id}", galaxy);
})
.WithName("PostGalaxy")
.WithOpenApi();

app.MapPut("/galaxy/update/{id}/{pkey}" , async (string id, string partitionKey, GalaxyUpdate update, CosmosDbService db) => {
    await db.UpdateGalaxyAsync(id, partitionKey, update);
    var galaxy = await db.GetGalaxyByIdAsync(id);
    if (galaxy == null)
        return Results.NotFound("Galaxy not found");
    return Results.Ok($"/galaxy/{galaxy.Id}", galaxy);
})
.WithName("UpdateGalaxy")
.WithOpenApi();

app.Run();

