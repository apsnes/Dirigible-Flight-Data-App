using AutoMapper;
using FlightApp.Entities;
using FlightApp.Repository;
using FlightAppLibrary.Models.Dtos;

namespace FlightApp.Service
{
    public interface INotesService
    {
        List<Note> GetNotes();
        NoteDto AddNote(NoteDto noteDto);
        List<NoteDto> GetNotesByIataAndDateTime(string flightIata, DateTime dateTimeScheduled);
        NoteDto GetNoteById(int id);
        List<NoteDto> GetNotesByUserId(string userId);
        Note DeleteNoteById(int id);
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

        public NoteDto GetNoteById(int id)
        {
            Note note = _notesRepository.GetNoteById(id);
            NoteDto noteDto = new NoteDto()
            {
                NoteId = note.NoteId,
                FlightIata = note.FlightIata,
                ScheduledDeparture = note.ScheduledDeparture,
                UserId = note.UserId,
                NoteText = note.NoteText,
                TimeStamp = note.TimeStamp,
                User = new UserDTO()
                {
                    Id = note.User!.Id,
                    Pronouns = note.User.Pronouns,
                    FirstName = note.User.FirstName,
                    LastName = note.User.LastName,
                    DisplayName = note.User.DisplayName ?? note.User.FirstName,
                    Email = note.User.Email!,
                    PhoneNumber = note.User.PhoneNumber,
                    Karma = note.User.Karma,
                    Avatar = note.User.Avatar,
                },
                Replies = note.Replies.Select(r => new ReplyDto()
                {
                    UserId = r.UserId,
                    NoteId = r.NoteId,
                    ReplyText = r.ReplyText,
                    TimeStamp = r.TimeStamp,
                    User = new UserDTO()
                    {
                        Id = r.User!.Id,
                        Pronouns = r.User.Pronouns,
                        FirstName = r.User.FirstName,
                        LastName = r.User.LastName,
                        DisplayName = r.User.DisplayName ?? r.User.FirstName,
                        Email = r.User.Email!,
                        PhoneNumber = r.User.PhoneNumber,
                        Karma = r.User.Karma,
                        Avatar = r.User.Avatar,
                    }
                }).ToList()
            };
            return noteDto;
        }

        public List<NoteDto> GetNotesByUserId(string userId)
        {
            List<Note> notes = _notesRepository.GetNotesByUserId(userId);
            if (notes != null && notes.Count > 0)
            {
                var res = notes.Select(n => new NoteDto()
                {
                    NoteId = n.NoteId,
                    FlightIata = n.FlightIata,
                    ScheduledDeparture = n.ScheduledDeparture,
                    UserId = n.UserId,
                    NoteText = n.NoteText,
                    TimeStamp = n.TimeStamp,
                    User = new UserDTO()
                    {
                        Id = n.User!.Id,
                        Pronouns = n.User.Pronouns,
                        FirstName = n.User.FirstName,
                        LastName = n.User.LastName,
                        DisplayName = n.User.DisplayName ?? n.User.FirstName,
                        Email = n.User.Email!,
                        PhoneNumber = n.User.PhoneNumber,
                        Karma = n.User.Karma,
                        Avatar = n.User.Avatar
                    },
                    Replies = n.Replies.Select(r => new ReplyDto()
                    {
                        UserId = r.UserId,
                        NoteId = r.NoteId,
                        ReplyText = r.ReplyText,
                        TimeStamp = r.TimeStamp,
                        User = new UserDTO()
                        {
                            Id = r.User!.Id,
                            Pronouns = r.User.Pronouns,
                            FirstName = r.User.FirstName,
                            LastName = r.User.LastName,
                            DisplayName = r.User.DisplayName ?? r.User.FirstName,
                            Email = r.User.Email!,
                            PhoneNumber = r.User.PhoneNumber,
                            Karma = r.User.Karma,
                            Avatar = r.User.Avatar
                        }
                    }).ToList()
                }).ToList();
                return res;
            }
            return [];
        }

        public List<NoteDto> GetNotesByIataAndDateTime(string flightIata, DateTime dateTimeScheduled)
        {
            var notes = _notesRepository.GetNotesByIataAndDateTime(flightIata, dateTimeScheduled);
            if (notes != null && notes.Count > 0)
            {
                var res = notes.Select(n => new NoteDto()
                {
                    NoteId = n.NoteId,
                    FlightIata = n.FlightIata,
                    ScheduledDeparture = n.ScheduledDeparture,
                    UserId = n.UserId,
                    NoteText = n.NoteText,
                    TimeStamp = n.TimeStamp,
                    User = new UserDTO()
                    {
                        Id = n.User!.Id,
                        Pronouns = n.User.Pronouns,
                        FirstName = n.User.FirstName,
                        LastName = n.User.LastName,
                        DisplayName = n.User.DisplayName ?? n.User.FirstName,
                        Email = n.User.Email!,
                        PhoneNumber = n.User.PhoneNumber,
                        Karma = n.User.Karma,
                        Avatar = n.User.Avatar
                    },
                    Replies = n.Replies.Select(r => new ReplyDto()
                    {
                        UserId = r.UserId,
                        NoteId = r.NoteId,
                        ReplyText = r.ReplyText,
                        TimeStamp = r.TimeStamp,
                        User = new UserDTO()
                        {
                            Id = r.User!.Id,
                            Pronouns = r.User.Pronouns,
                            FirstName = r.User.FirstName,
                            LastName = r.User.LastName,
                            DisplayName = r.User.DisplayName ?? r.User.FirstName,
                            Email = r.User.Email!,
                            PhoneNumber = r.User.PhoneNumber,
                            Karma = r.User.Karma,
                            Avatar = r.User.Avatar
                        }
                    }).ToList()
                }).ToList();
                return res;
            }
            return [];
        }

        //-------POST REQUESTS----------
        public NoteDto AddNote(NoteDto noteDto)
        {
            var note = new Note()
            {
                FlightIata = noteDto.FlightIata,
                ScheduledDeparture = (DateTime)noteDto.ScheduledDeparture!,
                UserId = noteDto.UserId,
                NoteText = noteDto.NoteText,
                TimeStamp = noteDto.TimeStamp,
                Replies = new()
            };

            var res = _notesRepository.AddNote(note);
            if (res == null) return null;
            noteDto.NoteId = res.NoteId;
            return noteDto;
        }

        //-------DELETE REQUESTS----------
        public Note DeleteNoteById(int id)
        {
            return _notesRepository.DeleteNoteById(id);
        }
    }
}
