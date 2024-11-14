using Microsoft.Azure.Cosmos;
using System.Net;
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

            Galaxy updatedGalaxy = new Galaxy{
                Id = id,
                GalaxyId = partitionKey,
                name = update.name ?? galaxy.name,
                type = update.type ?? galaxy.type,
                ageMlnYr = update.ageMlnYr ?? galaxy.ageMlnYr
            };

            if (update.otherNames != null && update.otherNames.Any())
                updatedGalaxy.otherNames = update.otherNames
                    .Concat(galaxy.otherNames != null ? galaxy.otherNames : Array.Empty<string>())
                    .Distinct()
                    .ToArray();
            else {
                updatedGalaxy.otherNames = galaxy.otherNames;
            }

            if (update.stars != null) {
                updatedGalaxy.stars = update.stars;
            }
            else if (galaxy.stars != null)
            {
                updatedGalaxy.stars = galaxy.stars;
            }

            if (update.location != null && galaxy.location != null) {
                updatedGalaxy.location = new Location {
                    constellation = update.location.constellation ?? galaxy.location.constellation,
                    distanceLyrs = update.location.distanceLyrs ?? galaxy.location.distanceLyrs
                };
            } else if (update.location != null) {
                updatedGalaxy.location = update.location;
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
            Console.WriteLine($"CosmosException occured while updating galaxy: {ce.Message}");
            throw;
        }
        catch (Exception ex) {
            Console.WriteLine($"Error occured while updating galaxy: {ex.Message}");
        }
    }


    public async Task<Galaxy> GetGalaxyByIdAsync(string id, string partitionKey) {
        try {
            var galaxy = await this.container.ReadItemAsync<Galaxy>(
                id, new PartitionKey(partitionKey)
            );
            return galaxy.Resource;
        } catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            Console.WriteLine($"Galaxy with id: {id}; not found: {ex.Message}");
            throw;
        } catch (Exception ce) {
            Console.WriteLine($"Error occured while retrieving galaxy: {ce.Message}");
            throw;
        }
    }


    public async Task<bool> DeleteGalaxyAsync(string id, string partitionKey) {
        try {
            await this.container.DeleteItemAsync<Galaxy>(id, new PartitionKey(partitionKey));
            return true;
        } catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound) {
            Console.WriteLine($"Galaxy {id} {partitionKey} to delete not found");
            return false;
            
        } catch (Exception ex) {
            Console.WriteLine($"Error occured while deleting: {ex.Message}");
            throw;
        }
    }
}