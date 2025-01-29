using FlightApp.Repository;

namespace FlightApp.Service
{
    public interface IFlightApiService
    {
        FlightResponse? GetFlightByIata(string iata);
    }

    public class FlightApiService : IFlightApiService
    {
        private readonly IFlightApiRepository _flightApiRepository;
        public FlightApiService(IFlightApiRepository flightApiRepository)
        {
            _flightApiRepository = flightApiRepository;
        }
        public FlightResponse? GetFlightByIata(string iata)
        {
            return _flightApiRepository.GetFlightByIata(iata);
        }
    }
}
