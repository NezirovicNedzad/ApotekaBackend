namespace ApotekaBackend.Dto_s
{
    public class TransakcijaDto
    {
        public int KlijentId { get; set; }
     

        public List<TransakcijaDetaljiDto> Detalji { get; set; } = new List<TransakcijaDetaljiDto>();
    }
}
