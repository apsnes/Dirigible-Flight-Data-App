using FlightApp.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightApp.Entities
{
    [PrimaryKey("FlightIata", "ScheduledDeparture")]
    public class Note
    {
        public string FlightIata { get; set; }
        public DateTime ScheduledDeparture { get; set; }


        [ForeignKey("Email")]
        public string UserEmail { get; set; }
        public ApplicationUser? User { get; set; }


        public string NoteText { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
