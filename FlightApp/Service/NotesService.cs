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
            var res =_mapper.Map <List<NoteDto>>(_notesRepository.GetNotesByIataAndDateTime(flightIata, dateTimeScheduled));
            return res;
        }


        //-------POST REQUESTS----------
        public Note AddNote(NoteDto noteDto)
        {
            var note = _mapper.Map<Note>(noteDto);
            return _notesRepository.AddNote(note);
        }
    }
}
