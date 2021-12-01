using IBM.Application.Interfaces;
using IBM.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IBM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatesController : ControllerBase
    {
        private readonly IRateService services;
        private readonly ILogger<RatesController> log;

        public RatesController(IRateService service, ILogger<RatesController> log)
        {
            this.services = service;
            this.log = log;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rate>>> GetAsync()
        {
            log.LogInformation("Iniciando consulta");
            var result = await services.GetRatesAsync();
            if (!result.Any())
            {
                string message = "No hay elementos para mostrar";
                log.LogInformation(message);
                return NotFound(message);
            }

            log.LogInformation("Todo salio OK.");
            return Ok(result);
        }
        [HttpGet("offline")]
        public async Task<ActionResult<IEnumerable<Rate>>> GetOfflineAsync()
        {
            log.LogInformation("Iniciando consulta");
            var result = await services.GetRatesAsync(true);
            if (!result.Any())
            {
                string message = "No hay elementos para mostrar";
                log.LogInformation(message);
                return NotFound(message);
            }

            log.LogInformation("Todo salio OK.");
            return Ok(result);
        }
    }
}
