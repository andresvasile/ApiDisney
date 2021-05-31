using ApiDisney.Models;

namespace Specifications
{
    public class ExistingCharacterByNameSpecification : BaseSpecification<Character>
    {
        public ExistingCharacterByNameSpecification(string name):
            base( x=>x.Name==name)
        {
            
        }
    }
}