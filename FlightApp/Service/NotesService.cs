using AutoMapper;
using FlightApp.Entities;
using FlightApp.Repository;
using FlightAppLibrary.Models.Dtos;
using Microsoft.AspNetCore.Components.Forms.Mapping;
using System.Collections.Generic;

namespace FlightApp.Service
{
    public interface INotesService
    {
        List<Note> GetNotes();
        Note AddNote(NoteDto noteDto);
        List<NoteDto> GetNotesByIataAndDateTime(string flightIata, DateTime dateTimeScheduled);
    }

    public class NotesService : INotesService
    {
        private readonly INotesRepository _notesRepository;
        private IMapper _mapper;
        public NotesService(INotesRepository notesRepository, IMapper mapper)
        {
            _notesRepository = notesRepository;
            _mapper = mapper;
        }

        //-------GET REQUESTS----------
        public List<Note> GetNotes()
        {
            return _notesRepository.GetNotes();
        }

        public List<NoteDto> GetNotesByIataAndDateTime(string flightIata, DateTime dateTimeScheduled)
        {
            var notes = _notesRepository.GetNotesByIataAndDateTime(flightIata, dateTimeScheduled);

            if (notes != null && notes.Count > 0)
            {
                var res = notes.Select(n => new NoteDto()
                {
                    FlightIata = n.FlightIata,
                    ScheduledDeparture = n.ScheduledDeparture,
                    UserId = n.UserId,
                    NoteText = n.NoteText,
                    TimeStamp = n.TimeStamp,
                    User = new UserDTO()
                    {
                        Id = n.User!.Id,
                        DisplayName = n.User.DisplayName ?? n.User.FirstName,
                        Karma = n.User.Karma,
                    }
                }).ToList();
                return res;
            }
            return new List<NoteDto>();
        }


        //-------POST REQUESTS----------
        public Note AddNote(NoteDto noteDto)
        {
            var note = new Note()
            {
                FlightIata = noteDto.FlightIata,
                ScheduledDeparture = (DateTime)noteDto.ScheduledDeparture!,
                UserId = noteDto.UserId,
                NoteText = noteDto.NoteText,
                TimeStamp = noteDto.TimeStamp,
            };

            return _notesRepository.AddNote(note);
        }
    }
}
