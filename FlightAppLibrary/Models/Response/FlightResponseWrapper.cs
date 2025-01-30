using System.Text.Json.Serialization;

namespace FlightAppLibrary.Models.Response
{
    public class FlightResponseWrapper
    {
        [JsonPropertyName("data")]
        public FlightResponse[] Data { get; set; }

    }
}
