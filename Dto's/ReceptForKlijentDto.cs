namespace ApotekaBackend.Dto_s
{
    public class ReceptForKlijentDto
    {
        public int Id { get; set; } 
        public string Zaglavlje { get; set; }

        public string Invocatio { get; set; }

        public string Ordinatio { get; set; }

        public string Subskricpija { get; set; }
        public string Farmaceut { get; set; }

        public string Uputstvo { get; set; }

        public int IdLeka { get; set; }

        public int IdFarmaceuta { get; set; }

        public int IdKlijenta { get; set; }

        public bool IsDoctorPresribed { get; set; }

        public string LekNaziv { get; set; }

        
    }
}
