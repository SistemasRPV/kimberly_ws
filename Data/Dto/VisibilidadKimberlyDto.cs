namespace kimberly_ws.Data.Dto
{
    public class VisibilidadKimberlyDto
    {
        public int? IdVisibilidad { get; set; }
        public string Rotulo { get; set; }
        public string Ean { get; set; }
        public int? Espacio { get; set; }
        public int? Ubicacion { get; set; }
        public int? Cantidad { get; set; }
        public int? IdReporte { get; set; }
        public int? IdVisitaReporte { get; set; }
        public int? EstadoReporte { get; set; }
        public int? CantidadReporte { get; set; }
        public int? IdMotivoReporte { get; set; }
        public string ObservacionesReporte { get; set; }
        public bool? DeletedReporte { get; set; }
    }
}