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
            CreateMap<Movie, MovieDto>()
                .ForMember(x=>x.GenreName,o=>o.MapFrom(s=>s.Genre.Name)).ReverseMap();
            CreateMap<Genre, GenreDto>();
            CreateMap<CharacterMovie, CharacterMovieDto>()
                .ForMember(x => x.CharacterName, o => o.MapFrom(s => s.Character.Name))
                //.ForMember(x => x.MovieName, o => o.MapFrom(s => s.Movie.Title))
                ;
            CreateMap<Character, CharacterSpecificDto>();
            CreateMap<Movie, MovieSpecificDto>();
        }
    }
}