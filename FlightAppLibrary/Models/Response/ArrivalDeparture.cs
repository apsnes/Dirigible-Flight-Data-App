using System.Text.Json.Serialization;

namespace FlightAppLibrary.Models.Response
{
    public class ArrivalDeparture
    {
        [JsonPropertyName("airport")]
        public string? Airport { get; set; }
        [JsonPropertyName("timezone")]
        public string? Timezone { get; set; }
        [JsonPropertyName("iata")]
        public string? Iata { get; set; }
        [JsonPropertyName("icao")]
        public string? Icao { get; set; }
        [JsonPropertyName("terminal")]
        public string? Terminal { get; set; }
        [JsonPropertyName("gate")]
        public string? Gate { get; set; }
        [JsonPropertyName("delay")]
        public int? Delay { get; set; }
        [JsonPropertyName("scheduled")]
        public DateTime? Scheduled { get; set; }
        [JsonPropertyName("estimated")]
        public DateTime? Estimated { get; set; }
        [JsonPropertyName("actual")]
        DateTime? Actual { get; set; }
        [JsonPropertyName("estimated_runway")]
        public DateTime? EstimatedRunway { get; set; }
        [JsonPropertyName("actual_runway")]
        public DateTime? ActualRunway { get; set; }
        [JsonPropertyName("baggage")]
        public string? Baggage { get; set; }

    }
}
