namespace ApotekaBackend.Dto_s
{
    public class TransDetaljiDto
    {
        public int Id { get; set; }
        public int IdLeka { get; set; }
        public int ?IdRecepta { get; set; }

        public int Kolicina {  get; set; }  
        public string? ReceptUputstvo { get; set; }
        public string  ImeLeka { get; set; }

    }
}
