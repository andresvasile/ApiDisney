using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDisney.Models
{
    public class Genre : BaseEntity
    {
        public int Id_Genre { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public virtual ICollection<Movie> Movies { get; set; }
    }
}
