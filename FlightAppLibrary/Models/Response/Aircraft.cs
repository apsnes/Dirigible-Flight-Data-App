using System.Text.Json.Serialization;

namespace FlightAppLibrary.Models.Response
{
    public class Aircraft
    {
        [JsonPropertyName("registration")]
        public string Registration {  get; set; }
        [JsonPropertyName("iata")]
        public string Iata { get; set; }
        [JsonPropertyName("icao")]
        public string Icao { get; set; }
        [JsonPropertyName("icao24")]
        public string Icao24 { get; set; }
    }
}
