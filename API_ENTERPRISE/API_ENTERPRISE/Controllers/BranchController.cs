using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_ENTERPRISE.Models.ResponsModels;
using API_ENTERPRISE.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

//Creator: BVALERO
//Controlador para obtener datos de las sedes de enterprise

namespace API_ENTERPRISE.Controllers
{
    [Produces("application/json")]
    [Route("api/branches")]
    public class BranchController : Controller
    {

        private readonly IBranchService _BranchService;


        public BranchController(IBranchService BranchService)
        {
            this._BranchService = BranchService;
        }


        // // GET /api/branches/
        /// <summary>
        /// Obtiene las sedes regitradas
        /// </summary>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<ResponsBranch>> GetBranch()
        {
            try
            {
                var sedes = await _BranchService.GetBranches();
                return Ok(sedes.Items);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // // GET /api/branches/{id}
        /// <summary>
        /// Obtiene las sedes regitradas
        /// </summary>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponsBranch>> GetBranchById(int id)
        {
            try
            {
                var sedes = await _BranchService.GetBranchesByID(id);
                return Ok(sedes);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
