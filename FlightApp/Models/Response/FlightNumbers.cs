using System.Text.Json.Serialization;

namespace FlightApp.Models.Response
{
    public class FlightNumbers
    {
        [JsonPropertyName("number")]
        public string Number {  get; set; }
        [JsonPropertyName("iata")]
        public string Iata { get; set; }
        [JsonPropertyName("icao")]
        public string Icao { get; set; }
        [JsonPropertyName("codeshared")]
        public Codeshared Codeshared { get; set; }
    }
}
