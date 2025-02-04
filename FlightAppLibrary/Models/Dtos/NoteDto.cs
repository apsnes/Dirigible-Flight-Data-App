using System.ComponentModel.DataAnnotations.Schema;

namespace FlightAppLibrary.Models.Dtos
{
    public class NoteDto
    {
        public int NoteId { get; set; }      
        public string UserEmail { get; set; }
        public int FlightId { get; set; }
        public string NoteText { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
