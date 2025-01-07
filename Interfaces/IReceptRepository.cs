using ApotekaBackend.Dto_s;
using ApotekaBackend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApotekaBackend.Interfaces
{
    public interface IReceptRepository
    {

        Task<List<Recept>> GetAll();
        Task<Recept> GetById(int id);   

        Task<Recept>Add(Recept recept); 

        Task<List<ReceptForKlijentDto>>GetByKlijent(int klijentId);
        Task <Recept> AddRandomRecept(int klijentId,int lekid);
        Task<ReceptBackgroundDto> GetNewReceipt();
    }
}
