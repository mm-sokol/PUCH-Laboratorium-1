using Newtonsoft.Json;



namespace CosmosApp.Models
{
    public class Planet {
        public string PlanetId {get; set;}
        public string name {get; set;}
        public double masSeptKg {get; set;}
        public double ageMlnYr {get; set;}
        public int moons {get; set;}
    }

    public class PlanetUpdate {
        public string PlanetId {get; set;}
        public string? name {get; set;}
        public double? masSeptKg {get; set;}
        public double? ageMlnYr {get; set;}
        public int? moons {get; set;}
    }
}