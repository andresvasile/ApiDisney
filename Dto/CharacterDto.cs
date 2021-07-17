using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiDisney.Models;

namespace ApiDisney.Dto
{
    public class CharacterDto
    {
        public int Id_Character { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public double Weight { get; set; }
        public string History { get; set; }
        public string Image { get; set; }
        //public ICollection<CharacterMovieDto> CharacterMovies { get; set; }
    }
}
