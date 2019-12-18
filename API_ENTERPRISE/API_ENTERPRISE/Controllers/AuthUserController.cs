using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_ENTERPRISE.Models.RequestModels;
using API_ENTERPRISE.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_ENTERPRISE.Controllers
{
    [Route("api/user")]
    public class AuthUserController : Controller
    {
        private readonly IAuthUserRepository UserRepository;

        public AuthUserController(IAuthUserRepository confi)
        {
            this.UserRepository = confi;
        }

        // GET: api/user
        [HttpPost]
        public async Task<IActionResult> GetUser([FromBody] RequestAuthUser request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var Users = await UserRepository.GetUser(request);
                return Ok(Users.Items);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
    }
}
