namespace ApiDisney.Dto
{
    public class CharacterMovieDto
    {
        public int Id_Character { get; set; }
        public CharacterDto Character { get; set; }
        public int Id_Movie { get; set; }
        public MovieDto Movie { get; set; }
    }
}