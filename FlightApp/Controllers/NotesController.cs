using FlightApp.Entities;
using FlightApp.Service;
using FlightAppLibrary.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FlightApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly INotesService _notesService;
        public NotesController(INotesService notesService)
        {
            _notesService = notesService;
        }

        //------GET REQUESTS------
        [HttpGet]
        public IActionResult GetNotes()
        {
            var result = _notesService.GetNotes();
            return result == null ? BadRequest("Unable to find any notes") : Ok(result);
        }

        [HttpGet("flight")]
        public IActionResult GetNotesByIataAndDateTime()
        {
            string? flightIata = Request.Query.ContainsKey("flight_iata") ? Request.Query["flight_iata"] : string.Empty;
            string? dateTimeScheduledString = Request.Query.ContainsKey("departure_time") ? Request.Query["departure_time"] : string.Empty;
            var dateTimeScheduled = DateTime.Parse(dateTimeScheduledString);
            var result = _notesService.GetNotesByIataAndDateTime(flightIata, dateTimeScheduled);
            return result == null ? BadRequest("Unable to find any notes") : Ok(result);
        }

        //-----POST REQUESTS-----
        [HttpPost]
        public IActionResult AddNote([FromBody] NoteDto noteDto)
        {
            var result = _notesService.AddNote(noteDto);
            return result == null ? BadRequest($"Unable to add comment") : Ok(result);
        }
    }
}
