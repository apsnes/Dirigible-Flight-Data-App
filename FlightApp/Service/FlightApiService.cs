using FlightApp.Models.Response;
using FlightApp.Repository;

namespace FlightApp.Service
{
    public interface IFlightApiService
    {
        FlightResponseWrapper? GetFlightByIata(string iata);
    }

    public class FlightApiService : IFlightApiService
    {
        private readonly IFlightApiRepository _flightApiRepository;
        public FlightApiService(IFlightApiRepository flightApiRepository)
        {
            _flightApiRepository = flightApiRepository;
        }
        public FlightResponseWrapper? GetFlightByIata(string iata)
        {
            return  _flightApiRepository.GetFlightByIata(iata).Result;
        }
    }
}
