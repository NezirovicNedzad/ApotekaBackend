using ApotekaBackend.Data;
using ApotekaBackend.Interfaces;
using ApotekaBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace ApotekaBackend.Repositories
{
    public class ReceptRepository(DataContext context) : IReceptRepository
    {
        public async Task<Recept> Add(Recept recept)
        {
            await context.Recepti.AddAsync(recept);
            return(recept); 
        }

        public async Task<List<Recept>> GetAll()
        {
            return await context.Recepti.ToListAsync();
        }

        public async Task<Recept?> GetById(int id)
        {
            var recept = await context.Recepti.FirstOrDefaultAsync(r=>r.Id==id);
            return recept;
        }
    }
}
