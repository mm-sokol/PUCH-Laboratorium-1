using Microsoft.Azure.Cosmos;
using CosmosApp.Models;


namespace CosmosApp.Services;

class CosmosDBService 
{
    private readonly Container container;


    public CosmosDBService(CosmosClient client, string databaseName, string containerName) 
    {
        container = client.GetContainer();
    }

    public async Task CreateItemAsync(Galaxy newGalaxy) {
        try
        {
            ItemResponse<Galaxy> newGalaxyResponse = await this.container.ReadItemAsync<Galaxy>(newGalaxy.Id, new PartitionKey(newGalaxy.PartitionKey));
            Console.WriteLine("Item in database with id: {0} already exists\n", newGalaxyResponse.Resource.Id);
        }
        catch(CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            ItemResponse<Galaxy> newGalaxyResponse = await this.container.CreateItemAsync<Galaxy>(newGalaxy.Id, new PartitionKey(newGalaxy.Id, newGalaxy.PartitionKey));
            Console.WriteLine("Item with id: {0} doesnt exist. Update aborted. Operation consumed {1} RUs.\n", newGalaxyResponse.Resource.Id, newGalaxyResponse.RequestCharge);
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
                Console.WriteLine($"\Red {galaxy}\n");
            }
        }
    }

    public async UpdateItemAsync(GalaxyUpdate galaxyUpdate) {
        try
        {
            ItemResponse<Galaxy> galaxyToUpdate = await this.container.GetItemAsync<Galaxy>(galaxyUpdate.Id, new PartitionKey(galaxyUpdate.GalaxyId));
            Console.WriteLine("Item with id: {0} exists\n", galaxyToUpdate.Resource.Id);

            if (galaxyUpdate.name != null)
                galaxyToUpdate.Resource.name = galaxyUpdate.name.Value;

            if (galaxyUpdate.type != null)
                galaxyToUpdate.Resource.type = galaxyUpdate.type.Value;


            if (galaxyUpdate.otherNames != null && galaxyUpdate.otherNames.Any()  )
                galaxyToUpdate.Resource.otherNames = galaxyToUpdate.Resource.otherNames.Concat(galaxyUpdate.otherNames.Value).Distinct().ToArray();

            if (galaxyUpdate.ageMlnYr != null)
                galaxyToUpdate.Resource.ageMlnYr = galaxyUpdate.ageMlnYr.Value;

            if (galaxyUpdate.location != null)
            {
                if (galaxyUpdate.location.Value.constellation != null)
                    galaxyToUpdate.Resource.location.constellation = galaxyUpdate.location.Value.constellation;

                if (galaxyUpdate.location.Value.ditanceLyrs != null)
                    galaxyToUpdate.Resource.location.ditanceLyrs = galaxyUpdate.location.Value.ditanceLyrs;
            }

            if (galaxyUpdate.stars != null)
            {
                
            }



            await container.ReplaceItemAsync(galaxyToUpdate.Resource, galaxyToUpdate.Resource.Id, new PartitionKey(galaxyToUpdate.Resource.GalaxyId));
            Console.WriteLine("Galaxy item updated successfully!");
        }
        catch(CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            ItemResponse<Galaxy> newGalaxyResponse = await this.container.CreateItemAsync<Galaxy>(newGalaxy, new PartitionKey(newGalaxy.PartitionKey));
            Console.WriteLine("Created item in database with id: {0} Operation consumed {1} RUs.\n", newGalaxyResponse.Resource.Id, newGalaxyResponse.RequestCharge);
        }
    }

    public async DeleteItemAsync(string id, Galaxy galaxy) {

    }


}