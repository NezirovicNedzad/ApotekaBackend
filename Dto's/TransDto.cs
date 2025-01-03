namespace ApotekaBackend.Dto_s
{
    public class TransDto
    {
        public int Id { get; set; }

        public int KlijentId { get; set; }
        public string KlijentName { get; set; }
        public int Cena { get; set; }
        public string DatumTransakcije { get; set; }

        public List<TransDetaljiDto>ProdajaDetalji {get;set;}=new List<TransDetaljiDto>();
    }
}
