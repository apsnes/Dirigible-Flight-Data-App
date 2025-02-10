
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FlightAppLibrary.Models.Dtos
{
    public class VoteReturnDto
    {
        [JsonPropertyName("userId")]
        public string? UserId { get; set; }

        [JsonPropertyName("value")]
        public int Value { get; set; }
    }
}
