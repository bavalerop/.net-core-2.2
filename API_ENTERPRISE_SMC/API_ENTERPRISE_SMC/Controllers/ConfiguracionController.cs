using System.Threading.Tasks;
using API_ENTERPRISE_SMC.Models.ResponsModels;
using API_ENTERPRISE_SMC.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

//Creator: BVALERO
//Controlador para obtener datos de configuracion de enterprise

namespace API_ENTERPRISE_SMC.Controllers
{
    [Produces("application/json")]
    [Route("api/config/")]

    public class ConfiguracionController : Controller
    {
        private readonly IConfiguracionService _ConfigService;



        ///<Summary>
        /// Constructor
        ///</Summary>
        public ConfiguracionController(IConfiguracionService ConfiService)
        {
            this._ConfigService = ConfiService;
        }

        // GET: api/config/
        /// <summary>
        /// Obtiene la configuraicon de trabajo por sede
        /// </summary>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        [AllowAnonymous]
        [HttpGet("getWorkByBranch")]
        public async Task<ActionResult<ResponsConfig>> GetConfiguracionSede()
        {
            try
            {
                var Config = await _ConfigService.GetJobBranch();
                return Ok(Config.Items);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/config/{key}
        /// <summary>
        /// Obtiene las configuraciones de Lab98.
        /// </summary>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        [Authorize]
        [HttpGet("{key}")]
        public async Task<ActionResult<ResponsConfig>> GetConfiguracion(string key)
        {
            try
            {
                var Config = await _ConfigService.GetConfig(key);
                return Ok(Config.Items);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
