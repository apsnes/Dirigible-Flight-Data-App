
namespace FlightAppLibrary.Models.Interfaces
{
    public interface IVotable
    {
        public string UserId { get; set; }
        public int Karma { get; set; }
    }
}

