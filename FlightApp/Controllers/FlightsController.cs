using FlightApp.Entities;
using FlightApp.Service;
using Microsoft.AspNetCore.Mvc;

namespace FlightApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlightsController : ControllerBase
    {
        private readonly IFlightsService _flightsService;
        public FlightsController(IFlightsService flightsService)
        {
            _flightsService = flightsService;
        }
        //------GET REQUESTS------
        [HttpGet]
        public IActionResult GetFlights()
        {
            var result = _flightsService.GetFlights();
            return result == null ? BadRequest("Unable to find any flights") : Ok(result);
        }

        [HttpGet("{iata}")]
        public IActionResult GetFlightByIata(string iata)
        {
            var result = _flightsService.GetFlightByIata(iata);
            return result == null ? BadRequest($"Could not find flight {iata}.") : Ok(result);
        }

        //-----POST REQUESTS-----
        [HttpPost]
        public IActionResult AddFlight([FromBody] Flight flight)
        {
            var result = _flightsService.AddFlight(flight);
            return result == null ? BadRequest($"Unable to add flight {flight.FlightId}") : Ok(result);
        }
    }
}