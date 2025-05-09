﻿using ApotekaBackend.Data;
using ApotekaBackend.Dto_s;
using ApotekaBackend.Interfaces;
using ApotekaBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ApotekaBackend.Controllers
{
    public class LekController(IUnitOfWork unitOfWork,UserManager<AppUser> userManager,IPhotoService photoService) : BaseApiController
    {
       
        [HttpGet("")]

        public async Task<ActionResult<List<Lek>>>GetAll()
        {
            var result=await unitOfWork.LekRepository.GetAll();

            if (result == null) BadRequest("Something went wrong!");

            return Ok(result);  
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LekDto>>GetById(int id)
        {
            var lek= await unitOfWork.LekRepository.GetById(id);
            if (lek == null) return NotFound("Vas lek ne postoji u sistemu!");

            LekDto lekDto = new()
            {
                Naziv = lek.Naziv,
                Opis = lek.Opis,
                DatumIsteka = lek.DatumIsteka,
                IdFarmaceuta = lek.IdFarmaceuta,
                Cena = lek.Cena,
                Kolicina = lek.Kolicina,
                NaRecept = lek.NaRecept,
              PhotoUrl = lek.PhotoUrl,  
                Proizvodjac = lek.Proizvodjac,

            };

            return Ok(lekDto); 

        }

        [HttpPost("")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> Add(LekDto lekDto)
        {
            //var Farmaceut = await userManager.FindByIdAsync(lekDto.IdFarmaceuta.ToString());

            //if (Farmaceut == null) return BadRequest("Nije pronadjen farmaceut dati");
            string photoUrl = null;
            if (lekDto.Photo != null && lekDto.Photo.Length > 0)
            {
                var photoResult = await photoService.AddPhotoAsync(lekDto.Photo);
                if (photoResult.Error != null) return BadRequest(photoResult.Error.Message);

                photoUrl = photoResult.SecureUrl.ToString();
            }
            Lek lek = new()
            {
              
                Naziv = lekDto.Naziv,
                Cena = lekDto.Cena,
                Kolicina = lekDto.Kolicina,
                DatumIsteka = lekDto.DatumIsteka,
                PhotoUrl = photoUrl,
                Proizvodjac = lekDto.Proizvodjac,
                Opis = lekDto.Opis,
                NaRecept = lekDto.NaRecept,
                IdFarmaceuta = lekDto.IdFarmaceuta,
            };
            await unitOfWork.LekRepository.AddLek(lek);
            await unitOfWork.Complete();

            return Ok(new { Message = "Uspesno dodat lek!", Naziv = lek.Naziv ,PhotoUrl=photoUrl});
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(LekDto lekDto,int id)
        {
            var lek=await unitOfWork.LekRepository.GetById(id);

            if (lek == null) { return BadRequest("Nije dobar user"); }
            lek.Opis = lekDto.Opis; 
            lek.NaRecept = lekDto.NaRecept;
            lek.Proizvodjac=lekDto.Proizvodjac; 
            lek.Cena = lekDto.Cena; 
            lek.Kolicina= lekDto.Kolicina; 
          
            await unitOfWork.LekRepository.UpdateLek(lek);
            await unitOfWork.Complete();
            return Ok(new { Message = "Uspesno uredjen lek!", Naziv = lek.Naziv });
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<Lek>>>GetLekByName(
            [FromQuery] string? naziv,
            [FromQuery] int pageNumber=1,
            [FromQuery] int pageSize=10
            )
        {
            List<Lek> lekovi;

            if (string.IsNullOrWhiteSpace(naziv))
            {
                // If no search term, return all records
                lekovi = await unitOfWork.LekRepository.GetAll();
            }
            else
            {
                // Search by naziv if provided
                lekovi = await unitOfWork.LekRepository.GetByNaziv(naziv);
            }
            int totalRecords = lekovi.Count();

             lekovi = lekovi
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

            var result = new
            {
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                lekovi = lekovi
            };

            return Ok(result);



           

        }

        [HttpGet("recept")]

        public async Task<ActionResult<List<Lek>>>GetLekoviNaRecept()
        {
            var lekoviNaRecept=await unitOfWork.LekRepository.GetAllNaRecept(); 
            if(lekoviNaRecept.Count==0)
            {
                return NotFound("Nema lekova na recept");
            }

            return Ok(lekoviNaRecept);
        }

        [HttpPut("zalihe")]

        public async Task<ActionResult<Lek>> ObnoviZalihe(int id, int kolicina)
        {
            await unitOfWork.LekRepository.ObnoviZalihe(id, kolicina);


            var result = await unitOfWork.Complete();

            if (result)
            {

                return Ok(new {Message="Uspesno dodati lekovi u sistemu"});  

            }
            else {

                return BadRequest("Los zahtev");
            }


        }



    }
}
