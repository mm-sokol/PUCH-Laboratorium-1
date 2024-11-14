using Microsoft.Azure.Cosmos;

using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Generic;
using System;
using System.Net;
using System.Threading.Tasks;

using CosmosApp.Models;


namespace CosmosApp.Services
{

public class CosmosDbService 
{
    private readonly Container container;


    public CosmosDbService(CosmosClient client, string databaseName, string containerName) 
    {
        container = client.GetContainer(databaseName, containerName);
    }

    public async Task CreateItemAsync(Galaxy newGalaxy) {
        try
        {
            ItemResponse<Galaxy> newGalaxyResponse = await this.container.ReadItemAsync<Galaxy>(newGalaxy.Id, new PartitionKey(newGalaxy.GalaxyId));
            Console.WriteLine("Item in database with id: {0} already exists\n", newGalaxyResponse.Resource.Id);
        }
        catch(CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            ItemResponse<Galaxy> newGalaxyResponse = await this.container.CreateItemAsync<Galaxy>(newGalaxy, new PartitionKey(newGalaxy.GalaxyId));
            Console.WriteLine("Item with id: {0} doesnt exist. Update aborted. Operation consumed {1} RUs.\n", newGalaxyResponse.Resource.Id, newGalaxyResponse.RequestCharge);
        }
    }


    public async Task<Galaxy> ReadItemByIdAsync(string id) {
            try
        {
            var partitionKey = "Andromeda";

            ItemResponse<Galaxy> response = await this.container.ReadItemAsync<Galaxy>(id, new PartitionKey(partitionKey));

            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return new Galaxy();
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while retrieving the galaxy.", ex);
        }
    }

    public async Task<List<Galaxy>> ReadItemsAsync(string partitionKeyValue) {

        var selectQuery = $"SELECT * FROM c WHERE c.GalaxyId = '{partitionKeyValue}'";
        Console.WriteLine($"Reading data: {selectQuery}\n");

        QueryDefinition queryDefinition = new QueryDefinition(selectQuery);

        FeedIterator<Galaxy> resultIterator = this.container.GetItemQueryIterator<Galaxy>(queryDefinition);
        List<Galaxy> galaxies = new List<Galaxy>();
        while (resultIterator.HasMoreResults)
        {
            FeedResponse<Galaxy> currentResultSet = await resultIterator.ReadNextAsync();
            foreach (Galaxy galaxy in currentResultSet)
            {
                galaxies.Add(galaxy);
                Console.WriteLine($"Red {galaxy}\n");
            }
        }
        return galaxies;
    }

    public async Task UpdateItemAsync(GalaxyUpdate galaxyUpdate)
    {
        try
        {
            // Query for the galaxy by ID and PartitionKey
            var query = new QueryDefinition("SELECT * FROM c WHERE c.id = @id")
                .WithParameter("@id", galaxyUpdate.Id);

            var iterator = this.container.GetItemQueryIterator<Galaxy>(
                query, 
                requestOptions: new QueryRequestOptions { PartitionKey = new PartitionKey(galaxyUpdate.GalaxyId) }
            );

            // Get the first result from the query (assumes a unique ID in the partition)
            var response = await iterator.ReadNextAsync();
            var galaxyToUpdate = response.FirstOrDefault();

            if (galaxyToUpdate == null)
            {
                throw new Exception($"Galaxy with ID {galaxyUpdate.Id} not found.");
            }

            // Log that the item was found
            Console.WriteLine("Item with id: {0} found.", galaxyToUpdate.Id);

            // Update properties if new values are provided
            if (galaxyUpdate.name != null)
                galaxyToUpdate.name = galaxyUpdate.name;

            if (galaxyUpdate.type != null)
                galaxyToUpdate.type = galaxyUpdate.type;

            if (galaxyUpdate.otherNames != null && galaxyUpdate.otherNames.Any())
                galaxyToUpdate.otherNames = (galaxyToUpdate.otherNames ?? Array.Empty<string>())
                    .Concat(galaxyUpdate.otherNames)
                    .Distinct()
                    .ToArray();

            if (galaxyUpdate.ageMlnYr != null)
                galaxyToUpdate.ageMlnYr = galaxyUpdate.ageMlnYr.Value;

            if (galaxyUpdate.location != null)
            {
                if (galaxyUpdate.location.constellation != null)
                    galaxyToUpdate.location.constellation = galaxyUpdate.location.constellation;

                if (galaxyUpdate.location.ditanceLyrs != null)
                    galaxyToUpdate.location.ditanceLyrs = galaxyUpdate.location.ditanceLyrs;
            }

            if (galaxyUpdate.stars != null)
                galaxyToUpdate.stars = galaxyUpdate.stars;

            // Update the item in Cosmos DB
            var responseToUpdate = await container.ReplaceItemAsync(
                galaxyToUpdate, 
                galaxyToUpdate.Id, 
                new PartitionKey(galaxyToUpdate.GalaxyId)
            );

            // Log the success
            Console.WriteLine($"Galaxy with id: {galaxyToUpdate.Id} updated successfully. Request charge: {responseToUpdate.RequestCharge} RU.");

        }
        catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            // Handle the case where the item was not found
            Console.WriteLine($"Galaxy with ID {galaxyUpdate.Id} not found.");
        }
        catch (CosmosException ex)
        {
            // Handle other Cosmos exceptions
            Console.WriteLine($"CosmosDB exception: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Handle unexpected errors
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }

    public async Task DeleteItemAsync(string Id, string GalaxyId) {

        ItemResponse<Galaxy> wakefieldFamilyResponse = await this.container.DeleteItemAsync<Galaxy>(Id,new PartitionKey(GalaxyId));
        Console.WriteLine($"Deleted galaxy [{Id},{GalaxyId}]\n");
    }


}

}