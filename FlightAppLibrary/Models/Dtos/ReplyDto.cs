using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FlightAppLibrary.Models.Dtos
{
    public class ReplyDto
    {
        [JsonPropertyName("userId")]
        public string UserId { get; set; }
        [JsonPropertyName("noteId")]
        public int NoteId { get; set; }
        [JsonPropertyName("replyText")]
        public string ReplyText { get; set; }
        [JsonPropertyName("timeStamp")]
        public DateTime TimeStamp { get; set; }

        [JsonPropertyName("user")]
        public UserDTO? User { get; set; }
    }
}
