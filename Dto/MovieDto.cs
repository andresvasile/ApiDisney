using System;
using System.Collections.Generic;
using ApiDisney.Models;

namespace ApiDisney.Dto
{
    public class MovieDto
    {
        public int Id_Movie { get; set; }
        public GenreDto Genre { get; set; }
        public int Id_Genre { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Rating { get; set; }
        public ICollection<CharacterMovieDto> CharacterMovies { get; set; }
    }
}