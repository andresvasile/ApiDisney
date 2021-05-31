using System.Collections.Generic;

namespace ApiDisney.Models
{
    public class Character : BaseEntity
    {
        public int Id_Character{ get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public double Weight{ get; set; }
        public string History { get; set; }
        public string Image { get; set; }
        public ICollection<CharacterMovie> CharacterMovies { get; set; }
    }
}