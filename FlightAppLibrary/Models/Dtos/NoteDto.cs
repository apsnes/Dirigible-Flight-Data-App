using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FlightAppLibrary.Models.Dtos
{
    public class NoteDto
    {
        [JsonPropertyName("flightIata")]
        public string FlightIata { get; set; }
        [JsonPropertyName("scheduledDeparture")]
        public DateTime? ScheduledDeparture { get; set; }
        [JsonPropertyName("userEmail")]
        public string UserEmail { get; set; }
        [JsonPropertyName("noteText")]
        public string NoteText { get; set; }
        [JsonPropertyName("timeStamp")]
        public DateTime TimeStamp { get; set; }
    }
}
