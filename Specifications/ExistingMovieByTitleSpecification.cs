using ApiDisney.Models;
using Specifications;

namespace ApiDisney.Specifications
{
    public class ExistingMovieByTitleSpecification : BaseSpecification<Movie>

    {
        public ExistingMovieByTitleSpecification(string title)
        :base(x=>x.Title==title)
        {
            
        }   
    }
}