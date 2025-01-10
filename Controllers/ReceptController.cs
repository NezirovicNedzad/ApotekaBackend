using ApotekaBackend.Dto_s;
using ApotekaBackend.Interfaces;
using ApotekaBackend.Models;
using ApotekaBackend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApotekaBackend.Controllers
{
   
    public class ReceptController(IUnitOfWork unitOfWork,IReceptDoctor receptService) : BaseApiController
    {


        [HttpGet("")]

        public async Task<ActionResult<List<Recept>>>GetRecepts()
        {

            var result=await unitOfWork.ReceptRepository.GetAll();  

            return Ok(result);  


        }
        [HttpGet("klijent/{idKlijenta}")]

        public async Task<ActionResult<List<ReceptForKlijentDto>>>GetReceptsForKlijent(int idKlijenta)
        {

            var result=await unitOfWork.ReceptRepository.GetByKlijent(idKlijenta);

            if (result.Count == 0)
            {
                return Ok();
            }

            return result;


        }
        [HttpGet("klijentPagin/{idKlijenta}")]

        public async Task<ActionResult> GetReceptsForKlijentPagin(int idKlijenta,
             [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {

            var recepti = await unitOfWork.ReceptRepository.GetByKlijent(idKlijenta);

            if (recepti.Count == 0)
            {
                return Ok();
            }
            int totalRecords = recepti.Count();

            recepti = recepti
           .Skip((pageNumber - 1) * pageSize)
           .Take(pageSize)
           .ToList();

            var result = new
            {
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                recepti = recepti
            };

            return Ok(result);
 


        }

        [HttpPost("")]

        public async Task<ActionResult>AddRecept(ReceptDto receptDto)
        {

            Recept recept=new()
            { Ordinatio = receptDto.Ordinatio,
            Subskricpija=receptDto.Subskripcija ,
            Uputstvo=receptDto.Uputstvo,
            Invocatio=receptDto.Invocatio,  
            Zaglavlje=receptDto.Zaglavlje,  
            IdFarmaceuta=receptDto.IdFarmaceuta,
            IdLeka=receptDto.IdLeka,    
            IdKlijenta=receptDto.IdKlijenta,    
            IsDoctorPresribed=receptDto.IsDoctorPresribed,  

            };   

           var result= await unitOfWork.ReceptRepository.Add(recept);

            await unitOfWork.Complete();

            return Ok(result);  

        }
        [HttpGet("check")]
        public async Task<IActionResult> CheckNewReceipt()
        {
            var newReceipt = await unitOfWork.ReceptRepository.GetNewReceipt();

            if (newReceipt != null)
            {
                return Ok(newReceipt); // Return the latest receipt or a flag
            }

            return NoContent(); // No new receipts
        }

        [HttpPost("random-add")]
        public async Task<IActionResult>RandomAdd(int idKlijenta,int idLeka)
        {
            var klijentId = await receptService.AddRecept();

            return Ok(klijentId);   

        }
    }
}
