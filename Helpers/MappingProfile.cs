using ApiDisney.Dto;
using ApiDisney.Models;
using AutoMapper;

namespace ApiDisney.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Character, CharacterDto>().ReverseMap();
            CreateMap<Movie, MovieDto>();
            CreateMap<Genre, GenreDto>();
            CreateMap<CharacterMovie, CharacterMovieDto>()
                .ForMember(x=>x.Character,o=>o.MapFrom(s=>s.Character))
                .ForMember(x => x.Movie, o => o.MapFrom(s => s.Movie));

        }
    }
}