using FlightApp.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightApp.Entities
{
    public class Reply
    {
        [Key]
        public int ReplyId { get; set; }

        [ForeignKey("User")]
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }

        [ForeignKey("Note")]
        public int NoteId { get; set; }
        public Note Note { get; set; }

        public string ReplyText { get; set; }
        public DateTime TimeStamp { get; set; }
        public int Karma { get; set; } = 0;
        public List<Vote>? Votes { get; set; }
    }
}
