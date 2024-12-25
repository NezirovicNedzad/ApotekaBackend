using ApotekaBackend.Models;

namespace ApotekaBackend.Interfaces
{
    public interface IReceptRepository
    {

        Task<List<Recept>> GetAll();
        Task<Recept> GetById(int id);   

        Task<Recept>Add(Recept recept); 
    }
}
