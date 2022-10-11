namespace kimberly_ws.Data.Dto
{
    public partial class PromocionWebDto
    {
        public int IdPromocion { get; set; }
        public string EAN { get; set; }
        public int Tipo { get; set; }
        public string? Descripcion { get; set; }
        public string FechaIni { get; set; }
        public string FechaFin { get; set; }
        public string Rotulo { get; set; }
        public bool Deleted { get; set; }
    }
}