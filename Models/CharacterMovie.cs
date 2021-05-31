namespace ApiDisney.Models
{
    public class CharacterMovie
    {
        public int Id_Character { get; set; }
        public Character Character { get; set; }
        public int Id_Movie { get; set; }
        public Movie Movie { get; set; }
    }
}