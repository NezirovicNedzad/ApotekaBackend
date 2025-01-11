namespace ApotekaBackend.Dto_s
{
    public class TransakcijaDto
    {
        public int KlijentId { get; set; }
     
        public int CenaTotal { get; set; }  
        public List<TransakcijaDetaljiDto> prodajaDetalji { get; set; } = new List<TransakcijaDetaljiDto>();
    }
}
