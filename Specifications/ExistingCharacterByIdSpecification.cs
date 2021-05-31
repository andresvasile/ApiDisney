using ApiDisney.Models;

namespace Specifications
{
    public class ExistingCharacterByIdSpecification : BaseSpecification<Character>
    {
        public ExistingCharacterByIdSpecification(int id) : base( x=>x.Id_Character==id)
        {
            
        }   
    }
}