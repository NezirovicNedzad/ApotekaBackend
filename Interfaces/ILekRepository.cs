using ApotekaBackend.Dto_s;
using ApotekaBackend.Models;

namespace ApotekaBackend.Interfaces
{
    public interface ILekRepository
    {

        Task<List<Lek>> GetAll();

        Task<Lek> GetById(int id);

        Task<Lek> AddLek(Lek lek);
        Task UpdateLek(Lek lek);
        Task<List<Lek>> GetByNaziv(string naziv);
        Task<List<Lek>> GetAllNaRecept();
        Task ObnoviZalihe(int id, int kolicina);
    }
}
