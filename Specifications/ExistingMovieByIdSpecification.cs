using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiDisney.Models;
using Specifications;

namespace ApiDisney.Specifications
{
    public class ExistingMovieByIdSpecification : BaseSpecification<Movie>
    {
        public ExistingMovieByIdSpecification(int id):base(x=>x.Id_Movie==id)
        {
            AddInclude(x=>x.CharacterMovies);
        }
    }
}
