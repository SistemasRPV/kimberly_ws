namespace kimberly_ws.Data.Dto
{
    public class FotoCentroKimberlyDto
    {
        public int IdCentro { get; set; }
        public int IdUsuario { get; set; }
        public string DireccionCalculada { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public string Base64Foto { get; set; }
    }
}