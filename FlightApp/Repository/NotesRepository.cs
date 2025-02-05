using FlightApp.Database;
using FlightApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlightApp.Repository
{
    public interface INotesRepository
    {
        List<Note> GetNotes();
        Note AddNote(Note note);
        List<Note> GetNotesByIataAndDateTime(string flightIata, DateTime dateTimeScheduled);
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

        public List<Note> GetNotesByIataAndDateTime(string flightIata, DateTime dateTimeScheduled)
        {
            try
            {
                var res = db.Notes.Where(n => n.FlightIata == flightIata && n.ScheduledDeparture == dateTimeScheduled).Include(n => n.User).Include(n => n.Replies).ToList();
                //var res = db.Notes.Where(n => n.FlightIata == flightIata && n.ScheduledDeparture == dateTimeScheduled).ToList();
                return res;
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
