using FlightApp.Models;

namespace FlightApp.Dtos
{
    public class FlightDto
    {
        public int Id { get; set; }
        public string FlightNumber { get; set; }
        public List<FlightNote> FlightNotes { get; set; }
    }
}
