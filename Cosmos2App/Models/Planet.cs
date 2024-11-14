using Newtonsoft.Json;



namespace Cosmos2App.Models
{
    public class Planet {
        public required string PlanetId {get; set;}
        public string? name {get; set;}
        public double? masSeptKg {get; set;}
        public double? ageMlnYr {get; set;}
        public int? moons {get; set;}
    }
}