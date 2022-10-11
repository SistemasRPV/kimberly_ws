namespace kimberly_ws.Data.Dto
{
    public partial class VisibilidadWebDto
    {
        public int IdVisibilidad { get; set; }
        public string Rotulo { get; set; }
        public string EAN { get; set; }
        public int Espacio { get; set; }
        public int Ubicacion { get; set; }
        public int Cantidad{ get; set; }
        public string FechaIni { get; set; }
        public string FechaFin { get; set; }
        public bool Deleted { get; set; }
    }
}