using AutoMapper;
using FlightApp.Entities;
using FlightApp.Repository;
using FlightAppLibrary.Models.Dtos;
using Microsoft.AspNetCore.Components.Forms.Mapping;
using System.Collections.Generic;

namespace FlightApp.Service
{
    public interface IRepliesService
    {
        List<Reply> GetReplies();
        List<ReplyDto> GetRepliesByNoteId(int noteId);
        ReplyDto PostReply(ReplyDto replyDto);
        List<ReplyDto> GetRepliesByUserId(string userId);
        Reply DeleteReplyById(int id);
    }

    public class RepliesService : IRepliesService
    {
        private readonly IRepliesRepository _repository;
        private IMapper _mapper;
        public RepliesService(IRepliesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //-------GetRequests----------
        public List<Reply> GetReplies()
        {
            return _repository.GetReplies();
        }
        public List<ReplyDto> GetRepliesByNoteId(int noteId)
        {
            var replies = _repository.GetRepliesByNoteId(noteId);

            var res = replies.Select(r => new ReplyDto()
            {
                ReplyId = r.ReplyId,
                UserId = r.UserId,
                NoteId = r.NoteId,
                ReplyText = r.ReplyText,
                TimeStamp = r.TimeStamp,
                User = new UserDTO()
                {
                    Id = r.User!.Id,
                    DisplayName = r.User.DisplayName ?? r.User.FirstName,
                    Karma = r.User.Karma,
                }
            }).ToList();

            return res;
        }
        public List<ReplyDto> GetRepliesByUserId(string userId)
        {
            var replies = _repository.GetRepliesByUserId(userId);

            var res = replies.Select(r => new ReplyDto()
            {
                ReplyId = r.ReplyId,
                UserId = r.UserId,
                NoteId = r.NoteId,
                ReplyText = r.ReplyText,
                TimeStamp = r.TimeStamp,
                Note = new NoteDto()
                {
                    NoteId = r.NoteId,
                    FlightIata = r.Note.FlightIata,
                    UserId = r.Note.UserId,
                    NoteText = r.Note.NoteText,
                    TimeStamp = r.Note.TimeStamp,
                },
                User = new UserDTO()
                {
                    Id = r.User!.Id,
                    DisplayName = r.User.DisplayName ?? r.User.FirstName,
                    Karma = r.User.Karma,
                }
            }).ToList();

            return res;
        }
        //-------Post Requests----------
        public ReplyDto PostReply(ReplyDto replyDto)
        {
            var reply = new Reply()
            {
                UserId = replyDto.UserId,
                NoteId = replyDto.NoteId,
                ReplyText = replyDto.ReplyText,
                TimeStamp = replyDto.TimeStamp
            };
            var response = _repository.AddReply(reply);
            if (response == null) return null;
            replyDto.ReplyId = response.ReplyId;
            return replyDto;
        }
        //-------Delete Requests----------
        public Reply DeleteReplyById(int id)
        {
            return _repository.DeleteReplyById(id);
        }
    }
}
