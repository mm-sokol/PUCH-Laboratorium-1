using Newtonsoft.Json;



namespace CosmosApp.Models
{
    public class Coordinates {
        public string? rightAscension {get; set;}
        public string? declination {get; set;}
    }

    public class CoordinatesUpdate {
        public string? rightAscension {get; set;}
        public string? declination {get; set;}
    }
}