namespace ApotekaBackend.Models
{
    public class TransakcijaDetalji
    {

        public int Id { get; set; } 
        public int IdTransakcije { get; set; }  
        public int IdLeka { get; set; }
        public int Kolicina { get; set; }  // Količina leka
        public int? ReceptId { get; set; }

        public Lek Lek { get; set; } = null!;           // Navigacija prema Leku
        public Recept Recept { get; set; }    // Navigacija prema Receptu
        
        public Transkacija? Transkacija { get; set; } // Navigacija prema ProdajaStandardni
             // Navigacija prema ProdajaRecept
    }
}
