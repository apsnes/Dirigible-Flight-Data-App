using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightApp.Entities
{
    [PrimaryKey("FlightId", "NoteId")]
    public class FlightNote
    {
        [ForeignKey("FlightId")]
        public int FlightId { get; set; }
        [ForeignKey("NoteId")]
        public int NoteId { get; set; }
    }
}
