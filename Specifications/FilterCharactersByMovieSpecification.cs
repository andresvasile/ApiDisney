using System.Linq;
using ApiDisney.Models;

namespace Specifications
{
    public class FilterCharactersByMovieSpecification : BaseSpecification<Character>
    {
        public FilterCharactersByMovieSpecification(int idMovie)
        :base(x=>x.CharacterMovies.Any(x=>x.Id_Movie==idMovie)){
            
        }
        
    }
}