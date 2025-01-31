namespace FlightAppLibrary.Models.Dtos
{
    public class FlightDto
    {
        public int Id { get; set; }
        public string FlightNumber { get; set; }
        public List<FlightNoteDto> FlightNotes { get; set; }
    }
}
