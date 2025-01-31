using FlightApp.Entities;
using FlightApp.Repository;

namespace FlightApp.Service
{
    public interface IFlightsService
    {
        List<Flight> GetFlights();
        Flight AddFlight(Flight flight);
    }

    public class FlightsService : IFlightsService
    {
        private readonly IFlightsRepository _flightsRepository;
        public FlightsService(IFlightsRepository flightsRepository)
        {
            _flightsRepository = flightsRepository;
        }

        //-----------GET REQUESTS-----------
        public List<Flight> GetFlights()
        {
            return _flightsRepository.GetFlights();
        }

        //-----------POST REQUESTS-----------
        public Flight AddFlight(Flight flight)
        {
            return _flightsRepository.AddFlight(flight);
        }
    }
}
