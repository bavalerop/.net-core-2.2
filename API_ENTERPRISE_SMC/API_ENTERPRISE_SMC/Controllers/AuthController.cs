using System.Threading.Tasks;
using API_ENTERPRISE_SMC.Models.RequestModels;
using API_ENTERPRISE_SMC.Models.ResponsModels;
using API_ENTERPRISE_SMC.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

//Creator: BVALERO
//Controlador para obtener datos de autenticacion y sedes

namespace API_ENTERPRISE_SMC.Controllers
{
    [Produces("application/json")]
    [Route("api/authentication")]
    public class AuthController : Controller
    {

        private readonly IAuthService AuthService;


        public AuthController(IAuthService authService) {
            this.AuthService = authService;
        }

        // GET: /api/authentication/branches
        /// <summary>
        /// Obtiene las sedes regitradas
        /// </summary>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        [AllowAnonymous]
        [HttpGet("branches")]
        public async Task<ActionResult<ResponsBranch>> GetBranch()
        {
            try
            {
                var sedes = await AuthService.GetBranches();
                return Ok(sedes.Items);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/authentication   
        /// <summary>
        /// Obtiene la autenticacion por usuario
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]


        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<ResponsAuth>> ResponsToken([FromBody] RequestAuthUser request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var ResponsToken = await AuthService.ResponsToken(request);
                return Ok(ResponsToken);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
