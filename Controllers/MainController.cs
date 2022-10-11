 using System.Threading.Tasks;
using kimberly_ws.Data.Dto;
using kimberly_ws.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace kimberly_ws.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        #region [Variables & Constructor]
        private MainRepository _repository;

        public MainController(MainRepository repository)
        {
            _repository = repository;
        }
        #endregion
        
        #region [Getters]
        [AllowAnonymous]
        [HttpGet("GetAppVersion")]
        public async Task<OkObjectResult> GetAppVersion()
        {
            return Ok(await _repository.GetAppVersion());
        }
        
        [HttpGet("GetTodosCentros")]
        public async Task<OkObjectResult> GetTodosCentros([FromHeader] string authorization, int idUser)
        {
            return Ok(await _repository.GetTodosCentros(authorization, idUser));
        }
        
        [HttpGet("GetTodosTipos")]
        public async Task<OkObjectResult> GetTodosTipos([FromHeader] string authorization)
        {
            return Ok(await _repository.GetTodosTipos(authorization));
        }
        
        [HttpGet("GetCentrosRuta")]
        public async Task<OkObjectResult> GetCentrosRuta([FromHeader] string authorization, int idUser)
        {
            return Ok(await _repository.GetCentrosRuta(authorization, idUser));
        }
        
        [HttpGet("GetVisita")]
        public async Task<OkObjectResult> GetVisita([FromHeader] string authorization, int idUser, int idCentro)
        {
            return Ok(await _repository.GetVisita(authorization, idUser, idCentro));
            
        }
        
        [HttpGet("GetFotos")]
        public async Task<OkObjectResult> GetFotos([FromHeader] string authorization, int idCentro, int categoria)
        {
            return Ok(await _repository.GetFotos(authorization, idCentro, categoria));
        }
        
        [HttpGet("GetContactos")]
        public async Task<OkObjectResult> GetContactos([FromHeader] string authorization, int idCentro)
        {
            return Ok(await _repository.GetContactos(authorization, idCentro));
        }
        
        [HttpGet("GetIncidencias")]
        public async Task<OkObjectResult> GetIncidencias([FromHeader] string authorization, int idCentro)
        {
            return Ok(await _repository.GetIncidencias(authorization, idCentro));
        }
        
        [HttpGet("GetTipoIncidencias")]
        public async Task<OkObjectResult> GetTipoIncidencias([FromHeader] string authorization)
        {
            return Ok(await _repository.GetTipoIncidencias(authorization));
        }
        
        [HttpGet("GetIncidenciasAcciones")]
        public async Task<OkObjectResult> GetIncidenciasAcciones([FromHeader] string authorization)
        {
            return Ok(await _repository.GetIncidenciasAcciones(authorization));
        }
        
        [HttpGet("GetPromociones")]
        public async Task<OkObjectResult> GetPromociones([FromHeader] string authorization, int idCentro)
        {
            return Ok(await _repository.GetPromociones(authorization, idCentro));
        }
        
        [HttpGet("GetReportePromociones")]
        public async Task<OkObjectResult> GetReportePromociones([FromHeader] string authorization, int idCentro)
        {
            return Ok(await _repository.GetReportePromociones(authorization, idCentro));
        }
        
        [HttpGet("GetProductos")]
        public async Task<OkObjectResult> GetProductos([FromHeader] string authorization, string rotulo)
        {
            return Ok(await _repository.GetProductos(authorization, rotulo));
        }
        
        [HttpGet("GetReporteProductos")]
        public async Task<OkObjectResult> GetReporteProductos([FromHeader] string authorization, int idCentro)
        {
            return Ok(await _repository.GetReporteProductos(authorization, idCentro));
        }
        
        [HttpGet("GetDocumentacion")]
        public async Task<OkObjectResult> GetDocumentacion([FromHeader] string authorization, int idCentro, int idUsuario)
        {
            return Ok(await _repository.GetDocumentacion(authorization, idCentro, idUsuario));
        }
        
        [HttpGet("GetVisibilidad")]
        public async Task<OkObjectResult> GetVisibilidad([FromHeader] string authorization, int idCentro, int idVisita)
        {
            return Ok(await _repository.GetVisibilidad(authorization, idCentro, idVisita));
        }
        
        [HttpGet("GetRelIncidenciaAccion")]
        public async Task<OkObjectResult> GetRelIncidenciaAccion([FromHeader] string authorization)
        {
            return Ok(await _repository.GetRelIncidenciaAccion(authorization));
        }

      
        #endregion
        
        #region [Setters]
        [HttpPost("SetContacto")]
        public async Task<ActionResult<string>> SetContacto([FromHeader] string authorization, [FromBody]ContactoKimberlyDto contacto)
        {
            return Ok(await _repository.SetContacto(authorization, contacto));
        }
        
        [HttpPost("SetFoto")]
        public async Task<ActionResult<string>> SetFoto([FromHeader] string authorization, [FromBody]FotoKimberlyDto foto)
        {
            return Ok(await _repository.SetFoto(authorization, foto));
        }
        
        [HttpPost("SetFotoCoordenadasCentro")]
        public async Task<ActionResult<string>> SetFotoCoordenadasCentro([FromHeader] string authorization, [FromBody]FotoCentroKimberlyDto foto)
        {
            return Ok(await _repository.SetFotoCoordenadasCentro(authorization, foto));
        }
        
        [HttpPost("SetReporteProducto")]
        public async Task<ActionResult<string>> SetReporteProducto([FromHeader] string authorization, [FromBody]ReporteProductoKimberlyDto[] reporte)
        {
            return Ok(await _repository.SetReporteProducto(authorization, reporte));
        }
        
        [HttpPost("SetReportePromocion")]
        public async Task<ActionResult<string>> SetReportePromocion([FromHeader] string authorization, [FromBody]ReportePromocionKimberlyDto[] reporte)
        {
            return Ok(await _repository.SetReportePromocion(authorization, reporte));
        }
        
        [HttpPost("SetVisibilidad")]
        public async Task<ActionResult<string>> SetVisibilidad([FromHeader] string authorization, [FromBody]ReporteVisibilidadKimberlyDto reporte)
        {
            return Ok(await _repository.SetVisibilidad(authorization, reporte));
        }
        
        [HttpPost("SetReporteIncidencia")]
        public async Task<ActionResult<string>> SetReporteIncidencia([FromHeader] string authorization, [FromBody]ReporteIncidenciaKimberlyDto reporte)
        {
            return Ok(await _repository.SetReporteIncidencia(authorization, reporte));
        }
        
        #endregion

    }
}