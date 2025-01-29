using AutoMapper;
using FlightApp.Dtos;
using FlightApp.Models;

namespace FlightApp.Mapper
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<Flight, FlightDto>().ReverseMap();
        }
    }
}
