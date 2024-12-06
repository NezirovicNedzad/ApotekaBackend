using ApotekaBackend.Models;

namespace ApotekaBackend.Interfaces
{
    public interface ITokenService
    {


        Task<string> CreateToken(AppUser user);
    }
}
