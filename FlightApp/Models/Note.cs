using System.ComponentModel.DataAnnotations.Schema;

namespace FlightApp.Models
{
    public class Note
    {
        public int Id { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        [ForeignKey("FlightNumber")]
        public List<FlightNote> FlightNotes { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
