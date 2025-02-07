
using FlightAppLibrary.Models.Enums;
using System.Text.Json.Serialization;

namespace FlightAppLibrary.Models.Dtos
{
    public class VoteDto
    {
        [JsonPropertyName("value")]
        public int Value { get; set; }

        [JsonPropertyName("commenterId")]
        public string CommenterId { get; set; }

        [JsonPropertyName("commentId")]
        public int CommentId { get; set; }

        [JsonPropertyName("commentType")]
        public CommentType CommentType { get; set; }
    }
}
