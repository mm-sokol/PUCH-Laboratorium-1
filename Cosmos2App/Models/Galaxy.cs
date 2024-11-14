using Newtonsoft.Json;



namespace Cosmos2App.Models
{
    public class Galaxy {
        [JsonProperty(PropertyName = "id")]
        public required string Id { get; set; }

        [JsonProperty(PropertyName = "GalaxyId")]
        public required string GalaxyId { get; set; }
        public string? name {get; set;}
        public string? type { get; set; }
        public string[]? otherNames { get; set; }
        public double? ageMlnYr { get; set; }
        public Location? location { get; set; }
        public Star[]? stars { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }   

}
