using ApotekaBackend.Data;
using ApotekaBackend.Dto_s;
using ApotekaBackend.Interfaces;
using ApotekaBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApotekaBackend.Controllers
{
    public class KlijentController(IUnitOfWork _unitOfWork) : BaseApiController
    {

        [HttpGet("")]

        public async Task<ActionResult<List<Klijent>>>GetAll()
        {
           
           var result=await _unitOfWork.KlijentRepository.GetAll(); 

            return Ok(result);
        }

        [HttpGet("{id:int}")]

        public async Task<ActionResult<Klijent>> GetById(int id)
        {

            var result = await _unitOfWork.KlijentRepository.GetById(id);
            if(result == null) 
                {
                return NotFound("Klijent nije pronadjen!");
                }

            return Ok(result);
        }

        [HttpPost("")]
        public async Task<ActionResult>AddKlijent(KlijentDto klijentDto)
        {

            Klijent klijent = new()
            {
                Adresa = klijentDto.Adresa,
                Ime = klijentDto.Ime,
                Prezime = klijentDto.Prezime,
                DatumRodjenja = klijentDto.DatumRodjenja,
                Email = klijentDto.Email,
                Telefon = klijentDto.Telefon,
                IdApotekara =klijentDto.IdApotekara,


            };

           var result= await _unitOfWork.KlijentRepository.AddKlijent(klijent);


            await _unitOfWork.Complete();
            return Ok(result);  

        }


        [HttpGet("search")]
        public async Task<ActionResult<List<Klijent>>> GetLekByName([FromQuery] string? naziv)
        {
            List<Klijent> klijenti;

            if (string.IsNullOrWhiteSpace(naziv))
            {
                // If no search term, return all records
                klijenti = await _unitOfWork.KlijentRepository.GetAll();
            }
            else
            {
                // Search by naziv if provided
                klijenti = await _unitOfWork.KlijentRepository.GetByNaziv(naziv);
            }



            return Ok(klijenti);

        }


    }
}
