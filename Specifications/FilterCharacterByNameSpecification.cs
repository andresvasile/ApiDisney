using ApiDisney.Models;

namespace Specifications
{
    public class FilterCharacterByNameSpecification : BaseSpecification<Character>
    {
        public FilterCharacterByNameSpecification(string name)
        :base(x=>x.Name.Contains(name)){
            
        }
    }
}