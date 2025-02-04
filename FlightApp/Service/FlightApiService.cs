using FlightAppLibrary.Models.Response;
using FlightApp.Repository;

namespace FlightApp.Service
{
    public interface IFlightService
    {
        List<FlightResponse?> GetFlightByIata(string iata);
        List<FlightResponse> GetFlightsByArrivalIataActive(string arr_iata);
        List<FlightResponse> GetIncidentFlights();
        List<FlightResponse> GetFlightsByRoute(string dep_iata, string arr_iata);
        List<FlightResponse> GetFlightsByDepartureAirportActive(string dep_iata);
    }

    public class FlightApiService : IFlightService
    {
        private readonly IFlightApiRepository _flightApiRepository;
        public FlightApiService(IFlightApiRepository flightApiRepository)
        {
            _flightApiRepository = flightApiRepository;
        }
        public List<FlightResponse?> GetFlightByIata(string iata)
        {
            return  _flightApiRepository.GetFlightByIata(iata).Result;
        }

        public List<FlightResponse> GetFlightsByArrivalIataActive(string arr_iata)
        {
            return _flightApiRepository.GetFlightByArrivalAirportActive(arr_iata).Result;
        }

        public List<FlightResponse> GetIncidentFlights()
        {
            return _flightApiRepository.GetIncidentFlights().Result;
        }

        public List<FlightResponse> GetFlightsByRoute(string dep_iata, string arr_iata)
        {
            return _flightApiRepository.GetFlightsByRoute(dep_iata, arr_iata).Result;
        }

        public List<FlightResponse> GetFlightsByDepartureAirportActive(string dep_iata)
        {
            return _flightApiRepository.GetFlightsByDepartureAirportActive(dep_iata).Result;
        }
    }
}
