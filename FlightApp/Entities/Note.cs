using FlightApp.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightApp.Entities
{
    public class Note
    {
        [Key]
        public int NoteId { get; set; }
        public string FlightIata { get; set; }
        public DateTime ScheduledDeparture { get; set; }


        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public string NoteText { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
