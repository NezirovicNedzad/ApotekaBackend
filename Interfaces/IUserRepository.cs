using ApotekaBackend.Models;

namespace ApotekaBackend.Interfaces
{
    public interface IUserRepository
    {
        Task<AppUser> GetByIdAsync(int id);
 
        Task AddAsync(AppUser user);

     
        Task UpdateAsync(AppUser user);
      
    }
}
