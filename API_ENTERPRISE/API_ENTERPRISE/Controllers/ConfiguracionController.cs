using System.Net;
using System.Threading.Tasks;
using API_ENTERPRISE.Models;
using API_ENTERPRISE.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

//Creator: BVALERO
//Controlador para obtener datos de configuracion de enterprise

namespace API_ENTERPRISE.Controllers
{
    [Route("api/config/")]

    public class ConfiguracionController : Controller
    {
        private readonly IConfiguracionRepository ConfigRepository;



        ///<Summary>
        /// Constructor
        ///</Summary>
        public ConfiguracionController(IConfiguracionRepository confi)
        {
            this.ConfigRepository = confi;
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


        [HttpGet]
        public async Task<ActionResult<Config>> GetConfiguracionSede()
        {
            try
            {
                var Config = await ConfigRepository.GetJobBranch();
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


        [HttpGet("{key}")]
        public async Task<ActionResult<Config>> GetConfiguracion(string key)
        {
            try
            {
                var Config = await ConfigRepository.GetConfig(key);
                return Ok(Config.Items);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
