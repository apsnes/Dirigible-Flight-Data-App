using FlightApp.Database;
using FlightApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlightApp.Repository
{
    public interface IRepliesRepository
    {
        Reply AddReply(Reply reply);
        List<Reply> GetReplies();
        List<Reply> GetRepliesByNoteId(int noteId);
        List<Reply> GetRepliesByUserId(string userId);
        Reply DeleteReplyById(int id);
    }

    public class RepliesRepository : IRepliesRepository
    {
        private readonly FlightAppDbContext db;
        public RepliesRepository(FlightAppDbContext _db)
        {
            db = _db;
        }

        // ------GET REQUESTS------
        public List<Reply> GetReplies()
        {
            try
            {
                return db.Replies.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<Reply>();
            }
        }
        public List<Reply> GetRepliesByNoteId(int noteId)
        {
            try
            {
                return db.Replies.Include(r => r.User).Where(r => r.NoteId == noteId).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<Reply>();
            }
        }
        public List<Reply> GetRepliesByUserId(string userId)
        {
            try
            {
                return db.Replies
                    .Include(r => r.Note)
                    .Include(r => r.User)
                    .Where(r => r.UserId == userId)
                    .ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<Reply>();
            }
        }
        // -----POST REQUESTS-----
        public Reply AddReply(Reply reply)
        {
            try
            {
                db.Replies.Add(reply);
                db.SaveChanges();
                return reply;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        // -----DELETE REQUESTS-----
        public Reply DeleteReplyById(int id)
        {
            try
            {
                db.Votes.RemoveRange(db.Votes.Where(v => v.ReplyId == id));

                var reply = db.Replies.Find(id);
                db.Replies.Remove(reply);
                db.SaveChanges();
                return reply;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}
