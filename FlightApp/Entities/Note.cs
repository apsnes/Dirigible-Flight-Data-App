using FlightApp.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightApp.Entities
{
    public class Note
    {
        [Key]
        public int NoteId { get; set; }


        [ForeignKey("Email")]
        public string UserEmail { get; set; }
        public ApplicationUser User { get; set; }


        [ForeignKey("FlightId")]
        public int FlightId { get; set; }
        public Flight Flight { get; set; }


        public string NoteText { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
