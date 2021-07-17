using System.Linq;
using System.Security.Cryptography.X509Certificates;
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