using ApiDisney.Models;

namespace Specifications
{
    public class FilterCharacterByAgeSpecification : BaseSpecification<Character>
    {
        public FilterCharacterByAgeSpecification(int age)
        :base(x=>x.Age==age){
            
        }        
    }
}