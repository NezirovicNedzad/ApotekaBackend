namespace ApotekaBackend.Models
{
    public class Transkacija
    {
        public int Id { get; set; }

        public int ?KlijentId {  get; set; } 
        public int Cena { get; set; }
        public DateTime DatumTransakcije { get; set; }
        public List<TransakcijaDetalji> ProdajaDetalji { get; set; } = new List<TransakcijaDetalji>(); // Lista lekova u prodaji
        public Klijent Klijent { get; set; }
    }
}
