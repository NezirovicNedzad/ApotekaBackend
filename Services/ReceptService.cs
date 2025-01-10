using ApotekaBackend.Data;
using ApotekaBackend.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace ApotekaBackend.Services
{
    public class ReceptService(IUnitOfWork _unitOfWork) : IReceptDoctor
    {

        

        public async Task<int>AddRecept()
        {
            var randomlekId = await LekId();
              int randomklijentId=await KlijentId();  




           await _unitOfWork.ReceptRepository.AddRandomRecept(randomklijentId, randomlekId);

            await _unitOfWork.Complete();

            return randomklijentId;
        }

        private async Task<int> LekId()
        {
            var lekovi = await _unitOfWork.LekRepository.GetAll();
            var lekoviId = lekovi.Where(l => l.NaRecept).Select(x => x.Id).ToList();
            var randomBr = 0;
            if (lekovi.Any()) // Ensure the list is not empty
            {

                if (lekoviId.Count > 0) // Check if there are any valid IDs to select
                {
                    Random random = new Random();
                    randomBr = random.Next(0, lekoviId.Count);  // Use lekoviId.Count to prevent out-of-bounds
                }
                else
                {
                    // Handle the case where there are no valid items
                    randomBr = 18;
                }
            }
            else
            {
                // Handle the case where lekovi is empty
                return 0;
            }



            return lekoviId[randomBr];

        }
        private async Task<int> KlijentId()
        {
            var klijenti = await _unitOfWork.KlijentRepository.GetAll();
            var klijentiId = klijenti.Select(x => x.Id).ToList();
            var randomBr = 0;
            if (klijenti.Any()) // Ensure the list is not empty
            {

                if (klijentiId.Count > 0) // Check if there are any valid IDs to select
                {
                    Random random = new Random();
                    randomBr = random.Next(0, klijentiId.Count);  // Use lekoviId.Count to prevent out-of-bounds
                }
                else
                {
                    // Handle the case where there are no valid items
                    randomBr = 18;
                }
            }
            else
            {
                // Handle the case where lekovi is empty
                return 0;
            }



            return klijentiId[randomBr];

        }
    }
}
