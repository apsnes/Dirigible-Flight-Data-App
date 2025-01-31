using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightAppLibrary.Models.Dtos
{
    
    public class FlightNoteDto
    {
        
        public int FlightId { get; set; }
        
        public int NoteId { get; set; }
    }
}
