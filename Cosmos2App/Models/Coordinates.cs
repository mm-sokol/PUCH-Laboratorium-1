using Newtonsoft.Json;



namespace Cosmos2App.Models
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