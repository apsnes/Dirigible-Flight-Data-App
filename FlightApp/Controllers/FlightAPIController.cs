using Azure;
using FlightApp.Helpers;
using FlightApp.Service;
using FlightAppLibrary.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Caching.Memory;
using System.Linq;

namespace FlightApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlightApiController : ControllerBase
    {
        private readonly IFlightService _flightApiService;
        private readonly IMemoryCache _cache;
        public FlightApiController(IFlightService flightApiService, IMemoryCache memoryCache)
        {
            _flightApiService = flightApiService;
            _cache = memoryCache;
        }

        [HttpGet("{iata}")]
        //[Authorize(Roles = "Customer")]
        public IActionResult GetFlightByIata(string iata)
        {
            var result = _flightApiService.GetFlightByIata(iata);
            return result == null ? BadRequest("Could not find flight.") : Ok(result);
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
            string QueryKey = "incident";
            List<FlightResponse> result;
            if (!_cache.TryGetValue(QueryKey, out result))
            {
                result = _flightApiService.GetIncidentFlights();
                if(result != null)
                {
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(7));

                    _cache.Set(QueryKey, result, cacheEntryOptions);
                }
            }
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

        [HttpGet("search")]
        [EnableRateLimiting("token")]
        public IActionResult GetSearchResults(
            [FromQuery] string? arrivals, 
            [FromQuery] string? departures, 
            [FromQuery] string? flight_iata, 
            [FromQuery] string? date,
            [FromQuery] int page_number,
            [FromQuery] int page_size)
        {
            List<FlightResponse> result;      

            
            string QueryKey = QueryHash.CreateKey(arrivals, departures, flight_iata, date, page_number, page_size);

            if (!_cache.TryGetValue(QueryKey, out result))
            {

                if (!string.IsNullOrEmpty(flight_iata))
                {
                    var flightByIata = _flightApiService.GetFlightByIata(flight_iata);
                    if (flightByIata != null) result = [flightByIata];
                }
                else if (!string.IsNullOrEmpty(arrivals) && !string.IsNullOrEmpty(departures))
                {
                    result = _flightApiService.GetFlightsByRoute(departures, arrivals);
                }
                else if (!string.IsNullOrEmpty(arrivals))
                {
                    result = _flightApiService.GetFlightsByArrivalIataActive(arrivals);
                }
                else if (!string.IsNullOrEmpty(departures))
                {
                    result = _flightApiService.GetFlightsByDepartureAirportActive(departures);
                }

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(7));

                _cache.Set(QueryKey, result, cacheEntryOptions);
            }

            

            if (result != null && result!.Count > 0)
            {
                result = result!
                .Where(r => !string.IsNullOrEmpty(r.Flight.Iata))
                .Where(r => ((DateTime)r.Departure.Scheduled!).ToString("dd_MM_yyyy") == date)
                .ToList();

                if (page_number > 0 && page_size > 0)
                {
                    result = result.Skip(page_number - 1 * page_size)
                    .Take(page_size)
                    .ToList();
                }
            }

            if (result is null || result.Count <= 0) return BadRequest("No flights found.");
            return Ok(result);
        }
    }
}
