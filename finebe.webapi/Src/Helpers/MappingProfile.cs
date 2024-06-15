using AutoMapper;
using finebe.webapi.Src.Models.Trips;
using finebe.webapi.Src.Persistence.DomainModel;

namespace finebe.webapi.Src.Helpers;
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Trips
            CreateMap<Trip, TripRequestDto>().ReverseMap();
            CreateMap<Trip, TripResponseDto>().ReverseMap();
        }
    }
