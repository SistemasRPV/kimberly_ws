namespace kimberly_ws.Data.Dto
{
    public partial class ReportePromocionKimberlyDto
    {
        public int? IdRPromocion { get; set; }
        public int? IdVisita { get; set; }
        public int? IdPromocion { get; set; }
        public int? Estado { get; set; }
        public int? IdMotivo { get; set; }
        public string Observaciones { get; set; }
        public bool? Deleted { get; set; }
    }
}