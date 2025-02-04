namespace FlightAppLibrary.Models.Dtos
{
    public class FlightDto
    {
        public int FlightId { get; set; }
        public string FlightNumber { get; set; }
        public DateOnly Date { get; set; }
        public List<NoteDto> Notes { get; set; }
    }
}
