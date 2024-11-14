using Microsoft.Azure.Cosmos;
using Cosmos2App.Models;

namespace Cosmos2App.Services;

public class CosmosDbService {
    private readonly CosmosClient client;
    private readonly Container container;

    public CosmosDbService(
        string connectionParams,
        string databaseName,
        string containerName
    ) {
        this.client = new CosmosClient(connectionParams);
        this.container = this.client.GetContainer(databaseName, containerName);
    }


    public async Task<IEnumerable<Galaxy>> GetGalaxiesAsync()
    {
        var query = new QueryDefinition("SELECT * FROM c");
        var iterator = this.container.GetItemQueryIterator<Galaxy>(query);

        var results = new List<Galaxy>();
        while (iterator.HasMoreResults)
        {
            var response = await iterator.ReadNextAsync();
            results.AddRange(response);
        }

        return results;
    }

    public async Task CreateGalaxyAsync(Galaxy galaxy) {

        try
        {
            var response = await this.container.CreateItemAsync(galaxy, new PartitionKey(galaxy.GalaxyId));

            Console.WriteLine($"Created galaxy {response.Resource.GalaxyId}, id: {response.Resource.Id}");
        } catch (CosmosException ce) {
            Console.WriteLine($"Error occured while creating galaxy: {ce.Message}");
            throw;
        }
    }
}