namespace ApotekaBackend.Models
{
    public class Recept
    {
        public int Id { get; set; } 
        public string Zaglavlje { get; set; }   

        public string Invocatio { get; set; }   

        public string Ordinatio { get; set; }   

        public string Subskricpija { get; set; }    

        public string Uputstvo { get; set; }

        public int IdLeka { get; set; } 

        public int IdFarmaceuta { get; set; }   

        public int IdKlijenta {  get; set; }

        public bool IsDoctorPresribed { get; set; }
        public Klijent Klijent { get; set; } = null!;
        public AppUser Farmaceut { get; set; } = null!;
        public Lek Lek { get; set; } = null!;

        public List<TransakcijaDetalji> ProdajaDetalji { get; set; } = new List<TransakcijaDetalji>();

    }
}
