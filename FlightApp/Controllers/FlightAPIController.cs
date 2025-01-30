using FlightApp.Service;
using Microsoft.AspNetCore.Mvc;

namespace FlightApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlightApiController : ControllerBase
    {
        private readonly IFlightApiService _flightApiService;
        public FlightApiController(IFlightApiService flightApiService)
        {
            _flightApiService = flightApiService;
        }

        [HttpGet("{iata}")]
        public IActionResult GetFlightByIata(string iata)
        {
            var result = _flightApiService.GetFlightByIata(iata);
            return result == null ? BadRequest() : Ok(result);
        }

        [HttpGet("arrivals/{arr_iata}")]
        public IActionResult GetFlightsByArrivalIataActive(string arr_iata)
        {
            var result = _flightApiService.GetFlightsByArrivalIataActive(arr_iata);
            return result == null ? BadRequest() : Ok(result);
        }

        [HttpGet("incident")]
        public IActionResult GetIncidentFlights()
        {
            var result = _flightApiService.GetIncidentFlights();
            return result == null ? BadRequest() : Ok(result);
        }

        [HttpGet("route/{dep_iata}/{arr_iata}")]
        public IActionResult GetFlightsByRoute(string dep_iata, string arr_iata)
        {
            var result = _flightApiService.GetFlightsByRoute(dep_iata, arr_iata);
            return result == null ? BadRequest() : Ok(result);
        }

        [HttpGet("departures/{dep_iata}")]
        public IActionResult GetFlightsByDepartureAirportActive(string dep_iata)
        {
            var result = _flightApiService.GetFlightsByDepartureAirportActive(dep_iata);
            return result == null ? BadRequest() : Ok(result);
        }
    }
}
