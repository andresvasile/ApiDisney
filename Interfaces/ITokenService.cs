using ApiDisney.Identity;


namespace Interfaces
{
    public interface ITokenService
    {
         string CreateToken(AppUser user);
    }
}