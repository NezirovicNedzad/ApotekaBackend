using ApotekaBackend.Data;
using ApotekaBackend.Interfaces;
using ApotekaBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace ApotekaBackend.Repositories
{
    public class KlijentRepository(DataContext _context) : IKlijentRepository
    {
        public async Task<Klijent> AddKlijent(Klijent klijent)
        {
            await _context.Klijenti.AddAsync(klijent);
            return klijent; 
            
        }

        public async Task<List<Klijent>> GetAll()
        {
            return await _context.Klijenti.ToListAsync();   
        }

        public async Task<Klijent?> GetById(int id)
        {
            return await _context.Klijenti.FirstOrDefaultAsync(k => k.Id == id);     
        }

        public async Task UpdateKlijent(Klijent klijent)
        {
           _context.Klijenti.Update(klijent);
        }
    }
}
