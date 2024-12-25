namespace ApotekaBackend.Models
{
    public class Klijent
    {
        public int Id { get; set; }
        public string Ime { get; set; }

        public string Prezime { get; set; }

        public string Adresa { get; set; }

        public string Telefon { get ; set; }    

        public string Email { get; set; }   

        public DateTime DatumRodjenja { get; set; }

        public int IdApotekara { get; set; }

        public ICollection<Recept> Recepti { get; set; } = null!;
        public AppUser Apotekar {  get; set; }  
        
    }
}
