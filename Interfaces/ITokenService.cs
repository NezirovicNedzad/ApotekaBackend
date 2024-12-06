using ApotekaBackend.Models;

namespace ApotekaBackend.Interfaces
{
    public interface ITokenService
    {


        string CreateToken(AppUser user);
    }
}
