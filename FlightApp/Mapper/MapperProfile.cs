using AutoMapper;
using FlightAppLibrary.Models.Dtos;
using FlightApp.Entities;

namespace FlightApp.Mapper
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<Flight, FlightDto>().ReverseMap();
            CreateMap<Note, NoteDto>().ReverseMap();
        }
    }
}
