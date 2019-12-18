using System.Threading.Tasks;
using API_QM.Core;
using API_QM.Core.Models;
using Microsoft.AspNetCore.Mvc;


namespace API_QM.Controllers
{
    [Route("api/config/")]
    public class ConfiguracionController : Controller
    { 
        private readonly IConfiguracionRepository ConfigRepository;


        public ConfiguracionController(IConfiguracionRepository confi) {
            this.ConfigRepository = confi;
        }
        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> GetConfiguracion()
        {
            try
            {
                var Config = await ConfigRepository.GetJobBranch();
                return Ok(Config);
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

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
