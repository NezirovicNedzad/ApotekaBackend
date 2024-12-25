using ApotekaBackend.Models;

namespace ApotekaBackend.Dto_s
{
    public class LekDto
    {

        public string Naziv { get; set; }
        public string Opis { get; set; }

        public string Proizvodjac { get; set; }

        public DateTime DatumIsteka { get; set; }

        public bool NaRecept { get; set; }
        public int Cena { get; set; }
        public int Kolicina { get; set; }


        public string PhotoUrl { get; set; }

        public int IdFarmaceuta { get; set; }
       
    }
}
