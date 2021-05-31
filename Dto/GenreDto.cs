using System.Collections.Generic;

namespace ApiDisney.Dto
{
    public class GenreDto
    {
        public int Id_Genre { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public ICollection<MovieDto> Movies { get; set; }
    }
}