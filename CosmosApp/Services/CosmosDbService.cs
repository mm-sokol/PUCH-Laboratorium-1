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

    public async Task CreateItemAsync(string id, Galaxy newGalaxy) {
        try
        {
            ItemResponse<Galaxy> newGalaxyResponse = await this.container.ReadItemAsync<Galaxy>(id, new PartitionKey(newGalaxy.PartitionKey));
            Console.WriteLine("Item in database with id: {0} already exists\n", newGalaxyResponse.Resource.Id);
        }
        catch(CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            ItemResponse<Galaxy> newGalaxyResponse = await this.container.CreateItemAsync<Galaxy>(newGalaxy, new PartitionKey(id, PartitionKey));
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

    public async UpdateItemAsync(string id, GalaxyUpdate galaxyUpdate) {
        try
        {
            ItemResponse<Galaxy> galaxyToUpdate = await this.container.GetItemAsync<Galaxy>(id, new PartitionKey(newGalaxy.GalaxyId));
            Console.WriteLine("Item with id: {0} exists\n", newGalaxyResponse.Resource.Id);




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