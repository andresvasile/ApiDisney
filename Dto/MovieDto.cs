using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using ApiDisney.Models;
using AutoMapper.Configuration.Annotations;

namespace ApiDisney.Dto
{
    public class MovieDto
    {
        public int Id_Movie { get; set; }
        public string GenreName { get; set; }
        public int Id_Genre{ get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public Rating Rating { get; set; }
        [JsonPropertyName("Characters")]
        public ICollection<CharacterMovieDto> CharacterMovies { get; set; }
    }
}