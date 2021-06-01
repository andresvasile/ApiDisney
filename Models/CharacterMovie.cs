namespace ApiDisney.Models
{
    public class CharacterMovie
    {
        public int Id_Character { get; set; }
        public virtual Character Character { get; set; }
        public int Id_Movie { get; set; }
        public virtual Movie Movie { get; set; }
    }
}