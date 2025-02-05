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
        Reply PostReply(ReplyDto replyDto);
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

        //-------PostRequests----------

        public Reply PostReply(ReplyDto replyDto)
        {
            var reply = new Reply()
            {
                UserId = replyDto.UserId,
                NoteId = replyDto.NoteId,
                ReplyText = replyDto.ReplyText,
                TimeStamp = replyDto.TimeStamp
            };
            //var reply = _mapper.Map<Reply>(replyDto);
            return _repository.AddReply(reply);
        }
    }
}
