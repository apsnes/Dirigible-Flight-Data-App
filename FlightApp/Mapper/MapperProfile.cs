using AutoMapper;
using FlightAppLibrary.Models.Dtos;
using FlightApp.Entities;
using FlightApp.Models;

namespace FlightApp.Mapper
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<Note, NoteDto>().ReverseMap();
            CreateMap<ApplicationUser, UserDTO>().ReverseMap();
        }
    }
}
