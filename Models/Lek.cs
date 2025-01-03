namespace ApotekaBackend.Models
{
    public class Lek
    {

        public int Id { get; set; } 

        public string Naziv { get; set; }    
        public string Opis { get; set; }
        
        public string Proizvodjac { get; set; }

        public DateTime DatumIsteka { get; set; }   

        public bool NaRecept {  get; set; } 
        public int Cena { get; set; }   
        public int Kolicina {  get; set; }  


        public string PhotoUrl {  get; set; }   

        public int IdFarmaceuta { get; set; }
        public AppUser Farmaceut { get; set; } = null!;
        public ICollection<Recept> Recepti { get; set; } = null!;
        public ICollection<TransakcijaDetalji> ProdajaDetalji { get; set; } = new List<TransakcijaDetalji>();
    }
}
