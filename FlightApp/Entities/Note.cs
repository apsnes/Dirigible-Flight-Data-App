using FlightApp.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightApp.Entities
{
    public class Note
    {
        [Key]
        public int NoteId { get; set; }


        [ForeignKey("UserId")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }


        [ForeignKey("FlightId")]
        public int FlightId { get; set; }
        public Flight Flight { get; set; }


        public DateTime TimeStamp { get; set; }
    }
}
