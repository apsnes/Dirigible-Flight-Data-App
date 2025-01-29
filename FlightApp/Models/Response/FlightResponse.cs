using System.Text.Json.Serialization;

namespace FlightApp.Models.Response
{
    public class FlightResponse
    {
        [JsonPropertyName("flight_date")]
        public string FlightDate { get; set; }
        [JsonPropertyName("flight_status")]
        public string FlightStatus { get; set; }
        [JsonPropertyName("departure")]
        public ArrivalDeparture Departure { get; set; }
        [JsonPropertyName("arrival")]
        public ArrivalDeparture Arrival { get; set; }
        [JsonPropertyName("airline")]
        public Airline Airline { get; set; }
        [JsonPropertyName("flight")]
        public FlightNumbers Flight { get; set; }
        [JsonPropertyName("aircraft")]
        public Aircraft Aircraft { get; set; }
        [JsonPropertyName("live")]
        public Live Live { get; set; }
    }
}
