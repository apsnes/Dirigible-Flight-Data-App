using System.Text.Json.Serialization;

namespace FlightApp.Models.Response
{
    public class FlightResponseWrapper
    {
        [JsonPropertyName("data")]
        public FlightResponse[] Data { get; set; }

    }
}
