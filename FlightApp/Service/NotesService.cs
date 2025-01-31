using FlightApp.Entities;
using FlightApp.Repository;

namespace FlightApp.Service
{
    public interface INotesService
    {
        List<Note> GetNotes();
        Note AddNote(Note note);
    }

    public class NotesService : INotesService
    {
        private readonly INotesRepository _notesRepository;
        public NotesService(INotesRepository notesRepository)
        {
            _notesRepository = notesRepository;
        }

        //-------GET REQUESTS----------
        public List<Note> GetNotes()
        {
            return _notesRepository.GetNotes();
        }

        //-------POST REQUESTS----------
        public Note AddNote(Note note)
        {
            return _notesRepository.AddNote(note);
        }
    }
}
