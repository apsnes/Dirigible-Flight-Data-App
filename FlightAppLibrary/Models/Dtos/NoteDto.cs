using System.ComponentModel.DataAnnotations.Schema;

namespace FlightAppLibrary.Models.Dtos
{
    public class NoteDto
    {
        public int NoteId { get; set; }      
        public string UserId { get; set; }
        public int FlightId { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
