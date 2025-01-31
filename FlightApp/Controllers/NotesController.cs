using FlightApp.Entities;
using FlightApp.Service;
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

        //-----POST REQUESTS-----
        [HttpPost]
        public IActionResult AddNote([FromBody] Note note)
        {
            var result = _notesService.AddNote(note);
            return result == null ? BadRequest($"Unable to add note {note.Id}") : Ok(result);
        }
    }
}
