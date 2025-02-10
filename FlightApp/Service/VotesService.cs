using AutoMapper;
using FlightApp.Entities;
using FlightApp.Repository;
using FlightAppLibrary.Models.Dtos;
using FlightAppLibrary.Models.Enums;

namespace FlightApp.Service
{
    public interface IVotesService
    {
        int AddVote(VoteDto voteDto, string userId);
    }

    public class VotesService : IVotesService
    {
        private readonly IVotesRepository _votesRepository;

        public VotesService(IVotesRepository votesRepository)
        {
            _votesRepository = votesRepository;
        }

        public int AddVote(VoteDto voteDto, string userId)
        {
            int? noteId = null;
            int? replyId = null;

            if(voteDto.CommentType == CommentType.Note)
            {
                noteId = voteDto.CommentId;
            }
            else if(voteDto.CommentType == CommentType.Reply)
            {
                replyId = voteDto.CommentId;
            }

            var vote = new Vote()
            {
                Value = voteDto.Value,
                UserId = userId,
                NoteId = noteId,
                ReplyId = replyId,
                CommentType = voteDto.CommentType
            };

            return _votesRepository.AddVote(vote, voteDto.CommenterId).Result;
        }
    }
}
