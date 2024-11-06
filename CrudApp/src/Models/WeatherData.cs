using Azure;
using Azure.Data.Tables;

namespace CrudApp.src.Models
{
    public class WeatherData : ITableEntity
    {
        public required string PartitionKey { get; set; }

        public required string RowKey { get; set; }

        public DateTimeOffset? Timestamp { get; set; }

        public ETag ETag { get; set; }

        public required double Temperature { get; set; }
        public required double Humidity { get; set; }
        public required double WindSpeed { get; set; }
    }

    public class WeatherUpdateData {
        public double? Temperature { get; set; }
        public double? Humidity { get; set; }
        public double? WindSpeed { get; set; }   
    }

    public class WeatherInsertData {
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public double WindSpeed { get; set; }   
    }
}
