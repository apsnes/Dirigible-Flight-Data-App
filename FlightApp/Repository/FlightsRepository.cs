using FlightApp.Database;
using FlightApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlightApp.Repository
{
    public interface IFlightsRepository
    {
        List<Flight> GetFlights();
        Flight AddFlight(Flight flight);
        Flight GetFlightByIata(string iata);
    }

    public class FlightsRepository : IFlightsRepository
    {
        private readonly FlightAppDbContext db;
        public FlightsRepository(FlightAppDbContext _db)
        {
            db = _db;
        }

        // ------GET REQUESTS------
        public List<Flight> GetFlights()
        {
            try
            {
                return db.Flights.Include(x => x.FlightNotes).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public Flight GetFlightByIata(string iata)
        {
            try
            {
                return db.Flights.Include(x => x.FlightNotes).FirstOrDefault(x => x.FlightNumber == iata);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        // -----POST REQUESTS-----
        public Flight AddFlight(Flight flight)
        {
            try
            {
                db.Flights.Add(flight);
                db.SaveChanges();
                return flight;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}
