using Newtonsoft.Json;



namespace CosmosApp.Models
{
    class Galaxy {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "GalaxyId")]
        public string GalaxyId { get; set; }
        public string name {get; set;}
        public string type { get; set; }
        public string[] otherNames { get; set; }
        public double ageMlnYr { get; set; }
        public Location location { get; set; }
        public Star[] stars { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    class GalaxyUpdate {
        public string Id { get; set; }
        public string GalaxyId { get; set; }
        public string? name {get; set;}
        public string? type { get; set; }
        public string[]? otherNames { get; set; }
        public double? ageMlnYr { get; set; }
        public LocationUpdate? location { get; set; }
        public StarUpdate[]? stars { get; set; }

        }    

}
