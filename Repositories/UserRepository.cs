using ApotekaBackend.Data;
using ApotekaBackend.Interfaces;
using ApotekaBackend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace ApotekaBackend.Repositories
{
    public class UserRepository(DataContext dataContext):IUserRepository
    {


        private readonly DataContext _dataContext = dataContext;

        public async Task AddAsync(AppUser user)
        {
            await _dataContext.Users.AddAsync(user);
           
        }

        public async Task<AppUser?> GetByIdAsync(int id)
        {
            var user=await _dataContext.Users.FindAsync(id);
           
            return user;
            
        }

        public async Task UpdateAsync(AppUser user)
        {
            _dataContext.Users.Update(user);
          
        }
    }
}
