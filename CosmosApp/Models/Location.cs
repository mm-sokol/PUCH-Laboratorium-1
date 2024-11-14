using Newtonsoft.Json;



namespace CosmosApp.Models
{
    public class Location {
        public string constellation {get; set;}
        public string ditanceLyrs {get; set;}
    }

    public class LocationUpdate {
        public string? constellation {get; set;}
        public string? ditanceLyrs {get; set;}
    }
}