using FlightApp.Database;
using FlightApp.Entities;
using FlightApp.Models;
using FlightAppLibrary.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace FlightApp.Repository
{
    public interface IVotesRepository
    {
        public Task<Vote?> AddVote(Vote vote, string commenterId);
    }

    public class VotesRepository : IVotesRepository
    {
        private readonly FlightAppDbContext _db;

        private readonly UserManager<ApplicationUser> _userManager;
        public VotesRepository(FlightAppDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<Vote?> AddVote(Vote vote, string commenterId)
        {
            try
            {
                if(_db.Votes.Any(v => v.UserId == vote.UserId && v.ReplyId == vote.ReplyId) 
                    || _db.Votes.Any(v => v.UserId == vote.UserId && v.NoteId == vote.NoteId))
                {
                    return null;
                }

                _db.Votes.Add(vote);

                if(vote.CommentType == CommentType.Note)
                {
                    var note = _db.Notes.FirstOrDefault(n => n.NoteId == vote.NoteId);
                    note!.Karma += vote.Value;
                }
                else
                {
                    var reply = _db.Replies.FirstOrDefault(r => r.ReplyId == vote.ReplyId);
                    reply!.Karma += vote.Value;
                }

                ApplicationUser? user = await _userManager.FindByIdAsync(commenterId);
                if (user != null)
                {
                    user.Karma += vote.Value;
                }

                _db.SaveChanges();
                return vote;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}
