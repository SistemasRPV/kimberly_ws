namespace kimberly_ws.Data.Dto
{
    public class ReporteProductoKimberlyDto
    {
        public int IdReporte { get; set; }	   
        public int IdVisita { get; set; }	    
        public string Ean { get; set; }	            
        public float Pvp { get; set; }	         
        public int Facing { get; set; }	        
        public bool? SinBalizaje { get; set; }	   
        public int Rotura { get; set; }	        
        public bool? FueraSurtido { get; set; }	
        public bool? Deleted { get; set; }	        	
    }
}