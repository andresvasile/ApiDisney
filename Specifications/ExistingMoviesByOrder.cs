using ApiDisney.Models;
using Specifications;

namespace ApiDisney.Specifications
{
    public class ExistingMoviesByOrder : BaseSpecification<Movie>
    {
        public ExistingMoviesByOrder(string sort)
        {
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "ASC":
                        AddOrderBy(f=>f.CreatedAt);
                        break;
                    case "DESC":
                        AddOrderByDescending(f=>f.CreatedAt);
                        break;
                    default:
                        AddOrderBy(n=>n.Title);
                        break;
                }
            }
        }
    }
}