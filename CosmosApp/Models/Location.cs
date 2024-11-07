using Newtonsoft.Json;



namespace CosmosApp.Models
{
    class Location {
        public string constellation {get; set;}
        public string ditanceLyrs {get; set;}
    }

    class LocationUpdate {
        public string? constellation {get; set;}
        public string? ditanceLyrs {get; set;}
    }
}