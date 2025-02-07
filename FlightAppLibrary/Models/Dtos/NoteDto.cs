using FlightAppLibrary.Models.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FlightAppLibrary.Models.Dtos
{
    public class NoteDto : IDisplayComment
    {
        [JsonPropertyName("noteId")]
        public int NoteId { get; set; }
        [JsonPropertyName("flightIata")]
        public string FlightIata { get; set; }
        [JsonPropertyName("scheduledDeparture")]
        public DateTime? ScheduledDeparture { get; set; }
        [JsonPropertyName("userId")]
        public string UserId { get; set; }
        [JsonPropertyName("noteText")]
        public string NoteText { get; set; }
        [JsonPropertyName("timeStamp")]
        public DateTime TimeStamp { get; set; }

        [JsonPropertyName("user")]
        public UserDTO? User { get; set; }

        [JsonPropertyName("replies")]
        public List<ReplyDto>? Replies { get; set; }
    }
}
