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

    public async Task UpdateGalaxyAsync(string id, string partitionKey, GalaxyUpdate update) {
        try
        {
            var galaxyResponse = await this.container.ReadItemAsync<Galaxy>(
                id, new PartitionKey(partitionKey)
            );
            var galaxy = galaxyResponse.Resource;

            Galaxy updatedGalaxy{
                Id: id,
                GalaxyId: partitionKey,
                name: update.name ?? galaxy.name,
                type: update.type ?? galaxy.type,
                ageMlnYr: update.ageMlnYr ?? galaxy.ageMlnYr,
                location: galaxy.location
            };

            if (update.OtherNames != null && update.OtherNames.Any())
                updatedGalaxy.OtherNames = update.OtherNames
                    .Concat(galaxy.OtherNames ?? new List<string>())
                    .Distinct()
                    .ToArray();
            else {
                updatedGalaxy.otherNames = galaxy.OtherNames;
            }

            if (update.location != null) {
                updatedGalaxy.location.constellation = update.location.constellation ?? galaxy.location.constellation;
                updatedGalaxy.location.distanceLyrs = update.location.distanceLyrs ?? galaxy.location.distanceLyrs;
            }

            if (update.stars != null) {
                updatedGalaxy.stars = update.stars;
            }

            await this.container.ReplaceItemAsync(
                updatedGalaxy, 
                galaxy.Id, 
                new PartitionKey(galaxy.GalaxyId)
            );
            Console.WriteLine($"Galaxy {partitionKey} has been updated");

        }
        catch (CosmosException ce) when (ce.StatusCode == HttpStatusCode.NotFound)
        {
            Console.WriteLine("Galaxy to update not found");
            throw;
        }
        catch (CosmosException ce) {
            Console.WriteLine($"Error occured while updating galaxy: {ce.Message}");
            throw;
        }
    }


    public async Task<Galaxy> GetGalaxyByIdAsync(string id, string partitionKey) {
        try {
            var galaxy = await this.container.ReadItemAsync<Galaxy>(
                id, new PartitionKey(partitionKey)
            );
            return galaxy.Resource;
        } catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            Console.WriteLine($"Galaxy with id: {id}; not found: {ex.Message}");
            return null;
        } catch (Exception ce) {
            Console.WriteLine($"Error occured while retrieving galaxy: {ce.Message}");
            throw;
        }
    }
}