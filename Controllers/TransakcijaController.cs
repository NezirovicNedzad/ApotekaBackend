using ApotekaBackend.Data;
using ApotekaBackend.Dto_s;
using ApotekaBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApotekaBackend.Controllers
{

    public class TransakcijaController(DataContext _context) : BaseApiController
    {

        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] TransakcijaDto transkacijaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if the client exists
            var klijent = await _context.Klijenti.FindAsync(transkacijaDto.KlijentId);
            var cena = 0;
            if (klijent == null)
            {
                return BadRequest("Dati klijent ne postoji!");
            }

            foreach (var detail in transkacijaDto.prodajaDetalji)
            {

                // Validate Lek
                var lek = await _context.Lekovi.FindAsync(detail.IdLeka);
               
                if (lek == null)
                {
                    return BadRequest("Lek ne postoji!");
                }
                if(lek.Kolicina==0)
                {
                    return BadRequest("Nema na stanju!");
                }
                lek.Kolicina = lek.Kolicina - 1;
                 _context.Lekovi.Update(lek);
                cena += lek.Cena * detail.KolicinaProizvoda;
                // Check if Lek requires a prescription
                if (lek.NaRecept)
                {
                    var recept = await _context.Recepti
                      .FirstOrDefaultAsync(r => r.Id == detail.IdRecepta && r.IdKlijenta == transkacijaDto.KlijentId && r.IdLeka == detail.IdLeka);
                    if (recept == null)
                    {
                        return BadRequest($"Nepostojeci recept za lek '{lek.Naziv}'.Proveri da li dati recept postoji za odgovarajuci lek i za odgovarajuceg klijenta?");
                    }



                }
            }

            // Map DTO to the Transakcija entity
            var transaction = new Transkacija
            {
                KlijentId = transkacijaDto.KlijentId,
                Cena = cena,
                DatumTransakcije = DateTime.UtcNow,
                ProdajaDetalji = transkacijaDto.prodajaDetalji.Select(d => new TransakcijaDetalji
                {
                    IdLeka = d.IdLeka,
                    Kolicina = d.KolicinaProizvoda,
                    ReceptId = d.IdRecepta
                }).ToList()
            };

            _context.Transakcije.Add(transaction);

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { Message = "Uspesno dodata transakcija!", id = transaction.Id});
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet]

        public async Task<ActionResult<List<TransDto>>> GetTrans()
        {

            var transakcije = await _context.Transakcije
            .Include(t => t.ProdajaDetalji)
                .ThenInclude(d => d.Lek) // Include associated Lek
            .Include(t => t.ProdajaDetalji)
                .ThenInclude(d => d.Recept) // Include associated Recept
            .Include(t => t.Klijent).ToListAsync();

            var transactionDtos = transakcije.Select(transaction => new TransDto
            {
                Id = transaction.Id,
                KlijentId = (int)transaction.KlijentId,
                KlijentName = transaction.Klijent.Ime + ' '+ transaction.Klijent.Prezime,
                Cena = transaction.Cena,
                DatumTransakcije = transaction.DatumTransakcije.ToString(),
                ProdajaDetalji = transaction.ProdajaDetalji.Select(d => new TransDetaljiDto
                {
                    Id = d.Id,
                    IdLeka = d.IdLeka,
                    ImeLeka = d.Lek.Naziv,
                    KolicinaProizvoda = d.Kolicina,
                    IdRecepta = d.ReceptId,
                    ReceptUputstvo = d.Recept?.Uputstvo
                }).ToList()
            }).ToList();

            return Ok(transactionDtos);// Include associated Klijent






        }
        [HttpGet("{id}")]

        public async Task<ActionResult<TransDto>> GetTrans(int id)
        {

            if(await _context.Transakcije.FirstOrDefaultAsync(t=>t.Id==id)==null)
            {
                return BadRequest("Ne postoji takva transakcija");
            }

            var transakcija = await _context.Transakcije
            .Include(t => t.ProdajaDetalji)
                .ThenInclude(d => d.Lek) // Include associated Lek
            .Include(t => t.ProdajaDetalji)
                .ThenInclude(d => d.Recept) // Include associated Recept
            .Include(t => t.Klijent).FirstOrDefaultAsync(x => x.Id == id);


            var transactionDto = new TransDto
            {
                Id = transakcija.Id,
                KlijentId = (int)transakcija.KlijentId,
                KlijentName = transakcija.Klijent.Ime +' '+ transakcija.Klijent.Prezime,
                Cena = transakcija.Cena,
                DatumTransakcije = transakcija.DatumTransakcije.ToString(),
                ProdajaDetalji = transakcija.ProdajaDetalji.Select(d => new TransDetaljiDto
                {
                    Id = d.Id,
                    IdLeka = d.IdLeka,
                    ImeLeka = d.Lek.Naziv,
                    KolicinaProizvoda = d.Kolicina,
                    IdRecepta = d.ReceptId,
                    ReceptUputstvo = d.Recept?.Uputstvo
                }).ToList()
            };
        

            return Ok(transactionDto);// Include associated Klijent






        }

        [HttpGet("popust/{id}")]

        public async Task<ActionResult>GetCena(int id,[FromQuery]int cenaTotal)
        {
             DateTime lastMonth = DateTime.Now.AddMonths(-1);

            var transakcijeKlijent=await _context.Transakcije.Where(x=>x.KlijentId==id).ToListAsync();    
            // Find a transaction that matches the criteria
            var transaction = transakcijeKlijent
                .Where(t => t.DatumTransakcije >= lastMonth && t.Cena > 5000)
                .FirstOrDefault();

            if (transaction != null)
            {
                // Calculate 80% of the price
                decimal cena = cenaTotal * 0.85M;
                return Ok( new {reducedCena =cena, popust=true});
            }

            return Ok(new { message="Nemate pravo popusta" ,popust=false});



        }


    }
}
