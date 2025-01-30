using System.Text.Json.Serialization;

namespace FlightAppLibrary.Models.Response
{
    public class Live
    {
        [JsonPropertyName("updated")]
        public DateTime Updated { get; set; }
        [JsonPropertyName("latitude")]
        public decimal Latitude { get; set; }
        [JsonPropertyName("longitude")]
        public decimal Longitude { get; set; }
        [JsonPropertyName("altitude")]
        public double Altitude { get; set; }
        [JsonPropertyName("direction")]
        public double Direction { get; set; }
        [JsonPropertyName("speed_horizontal")]
        public double SpeedHorizontal { get; set; }
        [JsonPropertyName("speed_vertical")]
        public double SpeedVertical { get; set; }
        [JsonPropertyName("is_ground")]
        public bool IsGround { get; set; }

    }
}
