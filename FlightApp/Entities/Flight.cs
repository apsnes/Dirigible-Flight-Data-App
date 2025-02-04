using System.ComponentModel.DataAnnotations;

namespace FlightApp.Entities
{
    public class Flight
    {
        [Key]
        public int FlightId { get; set; }
        public string FlightNumber { get; set; }
        public List<Note> Notes { get; set; }
    }
}
