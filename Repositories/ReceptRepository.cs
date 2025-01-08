using ApotekaBackend.Data;
using ApotekaBackend.Dto_s;
using ApotekaBackend.Interfaces;
using ApotekaBackend.Models;
using ApotekaBackend.Services;
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
                Subskripcija=x.Subskricpija,    
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

        public async Task<Recept> AddRandomRecept(int idKlijenta,int idLeka)
        {
            var random = new Random();

            var newRecept = new Recept
            {
                Ordinatio = "Random Ordinatio",
                Subskricpija = "Random Subskricpija",
                Uputstvo = "Random Uputstvo",
                Invocatio = "Random Invocatio",
                Zaglavlje = "Random Zaglavlje",
                IdFarmaceuta =28, // Random pharmacist
                IdLeka = idLeka, // Random medication
                IdKlijenta =idKlijenta, // Random client
                IsDoctorPresribed = true // Random boolean
            };
            await context.Recepti.AddAsync(newRecept);
            return newRecept;

        }

        public  async Task<ReceptBackgroundDto> GetNewReceipt()
        {
            var newReceipt = await context.Recepti.OrderByDescending(l => l.Id).Select(s=>new ReceptBackgroundDto
            {
                Zaglavlje = s.Zaglavlje,
                Invocatio=s.Invocatio,
                Ordinatio=s.Ordinatio,
                NazivLeka=s.Lek.Naziv,
                ImeKlijenta=s.Klijent.Ime+' '+s.Klijent.Prezime,
                Subskripcija=s.Subskricpija,
                Uputstvo=s.Uputstvo


            } ).FirstOrDefaultAsync();
            

            return newReceipt; // Return the latest receipt or a flag
            

        }
    }
}
