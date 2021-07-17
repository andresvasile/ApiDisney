using System;
using System.Collections.Generic;

namespace ApiDisney.Models
{
    public class Movie : BaseEntity
    {
        public int Id_Movie { get; set; }
        public virtual Genre Genre { get; set; }
        public int Id_Genre{ get; set; }
        public string Image{ get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt{ get; set; }
        public Rating? Rating { get; set; }
        public virtual ICollection<CharacterMovie> CharacterMovies { get; set; }
    }
}