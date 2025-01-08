using ApotekaBackend.Models;

namespace ApotekaBackend.Interfaces
{
    public interface IKlijentRepository
    {

        Task<List<Klijent>> GetAll();

        Task<Klijent> GetById(int id);

        Task<Klijent> AddKlijent(Klijent klijent);
        Task UpdateKlijent(Klijent lek);

        Task<List<Klijent>>GetByNaziv(string naziv);    
    }
}
