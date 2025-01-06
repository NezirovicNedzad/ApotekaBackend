using ApotekaBackend.Data;
using ApotekaBackend.Dto_s;
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

        public async Task<List<ReceptForKlijentDto>> GetByKlijent(int klijentId)
        { var recepti = await context.Recepti.Where(r => r.IdKlijenta == klijentId).Select(
            x=>new  ReceptForKlijentDto{
                Id=x.Id,    
                Zaglavlje=x.Zaglavlje,
                Ordinatio=x.Ordinatio,
                Invocatio=x.Invocatio,  
                Subskricpija=x.Subskricpija,    
                IdFarmaceuta=x.IdFarmaceuta,    
                IdLeka=x.IdLeka,
                    IdKlijenta=x.IdKlijenta,
                  IsDoctorPresribed=x.IsDoctorPresribed,    
                  LekNaziv=x.Lek.Naziv,
                  Uputstvo =x.Uputstvo,
                  Farmaceut=x.Farmaceut.Name+' '+x.Farmaceut.Surname
        
        
        }).ToListAsync();
            return recepti;
        }
    }
}
