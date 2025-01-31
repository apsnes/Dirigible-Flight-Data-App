using System.ComponentModel.DataAnnotations.Schema;

namespace FlightAppLibrary.Models.Dtos
{
    public class NoteDto
    {
        public int Id { get; set; }
        
        public int UserId { get; set; }
        
        public List<FlightNoteDto> FlightNotes { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
