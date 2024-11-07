using Newtonsoft.Json;



namespace CosmosApp.Models
{
    class Coordinates {
        public string rightAscension {get; set;}
        public string declination {get; set;}
    }

    class CoordinatesUpdate {
        public string? rightAscension {get; set;}
        public string? declination {get; set;}
    }
}