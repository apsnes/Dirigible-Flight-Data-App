using System.Text.Json.Serialization;

namespace FlightAppLibrary.Models.Response
{
    public class Airline
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("iata")]
        public string Iata { get; set; }
        [JsonPropertyName("icao")]
        public string Icao { get; set; }

    }
}
