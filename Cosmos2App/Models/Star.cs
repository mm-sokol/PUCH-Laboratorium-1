using Newtonsoft.Json;



namespace Cosmos2App.Models
{
    public class Star {
        public required string StarId {get; set;}
        public string? name {get; set;}
        public string? type {get; set;}
        public string? spectralType {get; set;}
        public Coordinates? coordinates {get; set;}
        public double? masSeptKg {get; set;}
        public double? ageMlnYr {get; set;}
        public Planet[]? planets {get; set;}
    }
}
