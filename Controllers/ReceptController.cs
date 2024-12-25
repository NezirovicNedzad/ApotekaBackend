using ApotekaBackend.Dto_s;
using ApotekaBackend.Interfaces;
using ApotekaBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApotekaBackend.Controllers
{
   
    public class ReceptController(IUnitOfWork unitOfWork) : BaseApiController
    {


        [HttpGet("")]

        public async Task<ActionResult<List<Recept>>>GetRecepts()
        {

            var result=await unitOfWork.ReceptRepository.GetAll();  

            return Ok(result);  


        }

        [HttpPost("")]

        public async Task<ActionResult>AddRecept(ReceptDto receptDto)
        {

            Recept recept=new()
            { Ordinatio = receptDto.Ordinatio,
            Subskricpija=receptDto.Subskricpija ,
            Uputstvo=receptDto.Uputstvo,
            Invocatio=receptDto.Invocatio,  
            Zaglavlje=receptDto.Zaglavlje,  
            IdFarmaceuta=receptDto.IdFarmaceuta,
            IdLeka=receptDto.IdLeka,    
            IdKlijenta=receptDto.IdKlijenta,    
            IsDoctorPresribed=receptDto.IsDoctorPresribed,  

            };   

           var result= await unitOfWork.ReceptRepository.Add(recept);

            return Ok(result);  

        }
    }
}
