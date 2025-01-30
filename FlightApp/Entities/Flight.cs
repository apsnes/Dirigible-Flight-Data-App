namespace FlightApp.Entities
{
    public class Flight
    {
        public int Id { get; set; }
        public string FlightNumber { get; set; }
        public List<FlightNote> FlightNotes { get; set; }
    }
}
