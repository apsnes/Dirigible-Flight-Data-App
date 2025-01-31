using FlightApp.Database;
using FlightApp.Entities;

namespace FlightApp.Repository
{
    public interface INotesRepository
    {
        List<Note> GetNotes();
        Note AddNote(Note note);
    }

    public class NotesRepository : INotesRepository
    {
        private readonly FlightAppDbContext db;
        public NotesRepository(FlightAppDbContext _db)
        {
            db = _db;
        }

        // ------GET REQUESTS------
        public List<Note> GetNotes()
        {
            try
            {
                return db.Notes.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        // -----POST REQUESTS-----
        public Note AddNote(Note note)
        {
            try
            {
                db.Notes.Add(note);
                db.SaveChanges();
                return note;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}
