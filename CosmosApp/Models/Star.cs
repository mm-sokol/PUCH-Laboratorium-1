using Newtonsoft.Json;



namespace CosmosApp.Models
{
    class Star {
        public string StarId {get; set;}
        public string name {get; set;}
        public string type {get; set;}
        public string spectralType {get; set;}
        public Coordinates coordinates {get; set;}
        public double masSeptKg {get; set;}
        public double ageMlnYr {get; set;}
        public Planet[] planets {get; set;}
    }

    class StarUpdate {
        public string StarId {get; set;}
        public string? name {get; set;}
        public string? type {get; set;}
        public string? spectralType {get; set;}
        public CoordinatesUpdate? coordinates {get; set;}
        public double? masSeptKg {get; set;}
        public double? ageMlnYr {get; set;}
        public PlanetUpdate[]? planets {get; set;}
    }
}
