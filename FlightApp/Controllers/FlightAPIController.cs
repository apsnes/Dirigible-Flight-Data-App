﻿using Azure;
using FlightApp.Service;
using FlightAppLibrary.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FlightApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlightApiController : ControllerBase
    {
        private readonly IFlightService _flightApiService;
        public FlightApiController(IFlightService flightApiService)
        {
            _flightApiService = flightApiService;
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

        [HttpGet("search")]
        public IActionResult GetSearchResults(
            [FromQuery] string? arrivals, 
            [FromQuery] string? departures, 
            [FromQuery] string? flight_iata, 
            [FromQuery] string? date,
            [FromQuery] int page_number,
            [FromQuery] int page_size)
        {
            List<FlightResponse> result = [];

            if (!string.IsNullOrEmpty(flight_iata))
            {
                result = [_flightApiService.GetFlightByIata(flight_iata)!];
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

            result = result
                .Where(r => r.Flight.Codeshared is null && !string.IsNullOrEmpty(r.Flight.Iata))
                .Where(r => ((DateTime)r.Departure.Scheduled!).ToString("dd_MM_yyyy") == date)
                .ToList();

            if (page_number > 0 && page_size > 0)
            {
                result = result.Skip(page_number - 1 * page_size)
                .Take(page_size)
                .ToList();
            }

            if (result is null || result.Count <= 0) return BadRequest("No flights found.");
            return Ok(result);
        }
    }
}
