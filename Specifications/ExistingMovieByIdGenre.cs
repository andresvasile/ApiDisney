using ApiDisney.Models;
using Specifications;

namespace ApiDisney.Specifications
{
    public class ExistingMovieByIdGenre : BaseSpecification<Movie>
    {
        public ExistingMovieByIdGenre(int idGenre)
        :base(x=>x.Id_Genre==idGenre){
            
        }
    }
}