using CosmosApp.Services;
using CosmosApp.Models;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;




namespace CosmosApp.Controllers;


[ApiController]
[Route("api/[controller]")]
public class GalaxyController : ControllerBase 
{
    private readonly CosmosDbService cosmosService;

    public GalaxyController(CosmosDbService cosmosDbService)
    {
        cosmosService = cosmosDbService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Galaxy>> GetGalaxy(string id)
    {
        var galaxy = await cosmosService.ReadItemByIdAsync(id);
        if (galaxy == null)
        {
            return NotFound();
        }
        return Ok(galaxy);
    }

    [HttpDelete("{id}/{partitionKey}")]
    public async Task<IActionResult> DeleteGalaxy(string id, string partitionKey)
    {
        await this.cosmosService.DeleteItemAsync(id, partitionKey);
        return NoContent();
    }
}
