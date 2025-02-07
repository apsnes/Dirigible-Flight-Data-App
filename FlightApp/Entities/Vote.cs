using FlightApp.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FlightAppLibrary.Models.Enums;

namespace FlightApp.Entities
{
    public class Vote
    {
        [Key]
        public int VoteId { get; set; }
        public int Value { get; set; }

        [ForeignKey("User")]
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }

        [ForeignKey("NoteId")]
        public int? NoteId { get; set; }
        public Note? Note { get; set; }

        [ForeignKey("ReplyId")]
        public int? ReplyId { get; set; }
        public Reply? Reply { get; set; }

        public CommentType CommentType { get; set; }
    }
}
