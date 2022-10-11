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
    public class LoginController : ControllerBase
    {
        #region [Variables & Constructor]
        private LoginRepository _repository;

        public LoginController(LoginRepository repository)
        {
            _repository = repository;
        }
        #endregion
        
        #region [Public]
        [AllowAnonymous]
        [HttpPost("Auth")]
        public async Task<ActionResult<AuthDto>> Authenticate([FromBody]LoginDto auth)
        {
            return Ok(await _repository.Authenticate(auth));
        }
        #endregion
        
    }
}