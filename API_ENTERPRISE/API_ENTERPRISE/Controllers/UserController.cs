using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_ENTERPRISE.Models.RequestModels;
using API_ENTERPRISE.Models.ResponsModels;
using API_ENTERPRISE.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

//Creator: BVALERO
//Controlador para obtener datos de usuarios

namespace API_ENTERPRISE.Controllers
{
    [Produces("application/json")]
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly IUserService _UserService;

        public UserController(IUserService UserService) {
            this._UserService = UserService;
        }

        // GET: /api/users
        /// <summary>
        /// Obtiene los usuarios registrados
        /// </summary>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<ResponsUser>> GetUsers()
        {
            try
            {
                var users = await this._UserService.GetUser();
                return Ok(users.Items);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: /api/users/{id}
        /// <summary>
        /// Obtiene los usuarios por id
        /// </summary>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponsUser>> GetUsersByID(int id)
        {
            try
            {
                var user = await this._UserService.GetUserByID(id);
                return Ok(user);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/users/getbybranchareas
        /// <summary>
        /// Obtiene los usuarios por sede y areas
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        [Authorize]
        [HttpPost("getbybranchareas")]
        public async Task<ActionResult<ResponsUserByBraSec>> ResponsToken([FromBody] RequestBranchSection request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var ResponsUsersBS = await _UserService.GetUserByBranSec(request);
                return Ok(ResponsUsersBS.Items);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
