using AutoMapper;
using FlightAppLibrary.Models.Dtos;
using FlightApp.Entities;

namespace FlightApp.Mapper
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<Note, NoteDto>().ReverseMap();
            CreateMap<Reply, ReplyDto>().ReverseMap();
        }
    }
}
