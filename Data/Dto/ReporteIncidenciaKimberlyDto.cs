namespace kimberly_ws.Data.Dto
{
    public class ReporteIncidenciaKimberlyDto
    {
        public bool Deleted { get; set; }
        public int Estado { get; set; }
        public int IdAccion { get; set; }
        public int IdIncidencia { get; set; }
        public int IdTipoIncidencia { get; set; }
        public int IdVisita { get; set; }
        public int IdVisitaModificacion { get; set; }
        public string Observaciones { get; set; }
    }
}