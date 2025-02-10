using FlightApp.Entities;
using FlightApp.Service;
using FlightAppLibrary.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("{id}")]
        public IActionResult GetNoteById(int id)
        {
            var result = _notesService.GetNoteById(id);
            return result == null ? BadRequest($"Unable to find note {id}") : Ok(result);
        }

        [HttpGet("user")]
        [Authorize]
        public IActionResult GetNotesByUser()
        {
            var userId = User.FindFirst("Id");
            string userIdValue = userId!.Value;
            var result = _notesService.GetNotesByUserId(userIdValue);
            return result == null ? BadRequest($"Unable to find any notes for user {userIdValue}") : Ok(result);
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

        //-----DELETE REQUESTS-----
        [HttpDelete("{id}")]
        public IActionResult DeleteNoteById(int id)
        {
            var result = _notesService.DeleteNoteById(id);
            return result == null ? BadRequest($"Unable to delete note {id}") : Ok(result);
        }

        [HttpGet("user/{userId}")]
        [Authorize]
        public IActionResult GetNotesByUserId(string userId)
        {
            var result = _notesService.GetNotesByUserId(userId);
            return result == null ? BadRequest($"Unable to find any notes for user {userId}") : Ok(result);
        }
    }
}
