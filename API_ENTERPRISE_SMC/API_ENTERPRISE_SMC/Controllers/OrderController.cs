using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_ENTERPRISE_SMC.Models.RequestModels;
using API_ENTERPRISE_SMC.Models.ResponsModels;
using API_ENTERPRISE_SMC.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

//Creator: BVALERO
//Controlador para obtener datos de usuarios

namespace API_ENTERPRISE_SMC.Controllers
{
    [Produces("application/json")]
    [Route("api/order")]
    public class OrderController : Controller
    {

        private readonly IOrderService _OrderService;

        ///<Summary>
        /// Constructor
        ///</Summary>
        public OrderController(IOrderService OrderService)
        {
            this._OrderService = OrderService;
        }


        // POST api/order
        /// <summary>
        /// Obtiene los usuarios por sede y areas
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[AllowAnonymous]
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ResponsOrder>> CreateOrder([FromBody] List<RequestOrder> request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var ResponsOrder = await _OrderService.CreateOrder(request);
                return Ok(ResponsOrder.Items);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
