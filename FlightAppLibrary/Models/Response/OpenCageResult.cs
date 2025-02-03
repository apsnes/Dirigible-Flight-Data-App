using System.Text.Json.Serialization;

namespace FlightAppLibrary.Models.Response
{
    public class OpenCageResult
    {
        [JsonPropertyName("geometry")]
        public Dictionary<string, decimal> Geometry { get; set; }
    }
}