﻿using ApotekaBackend.Data;
using ApotekaBackend.Dto_s;
using ApotekaBackend.Interfaces;
using ApotekaBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace ApotekaBackend.Repositories
{
    public class LekRepository(DataContext _context) : ILekRepository
    {
        public async Task<Lek> AddLek(Lek lek)
        {
           
           await _context.Lekovi.AddAsync(lek);

            return lek;

        }

        public async Task<List<Lek>> GetAll()
        {
            return await _context.Lekovi.ToListAsync();   
        }

        public async Task<Lek> GetById(int id)
        
        {
          var lek=  await _context.Lekovi.FirstOrDefaultAsync(x => x.Id == id);
           
            return lek; 
        }

        public async Task UpdateLek(Lek lek)
        {
            _context.Lekovi.Update(lek);
        }
    }
}
