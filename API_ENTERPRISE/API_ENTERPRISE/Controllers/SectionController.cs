using System.Threading.Tasks;
using API_ENTERPRISE.Models.ResponsModels;
using API_ENTERPRISE.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

//Creator: BVALERO
//Controlador para obtener datos de las areas de enterprise

namespace API_ENTERPRISE.Controllers
{
    [Produces("application/json")]
    [Route("api/areas")]
    public class SectionController : Controller
    {
        private readonly ISectionService _SectionService;
        public SectionController(ISectionService SectionService)
        {
            this._SectionService = SectionService;
        }

        // // GET /api/areas/
        /// <summary>
        /// Obtiene las areas regitradas
        /// </summary>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<ResponsSection>> GetBranch()
        {
            try
            {
                var areas = await _SectionService.GetSection();
                return Ok(areas.Items);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // // GET /api/areas/{id}
        /// <summary>
        /// Obtiene las areas regitradas
        /// </summary>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponsSection>> GetSectionById(int id)
        {
            try
            {
                var area = await _SectionService.GetSectionByID(id);
                return Ok(area);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
