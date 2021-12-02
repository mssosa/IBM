using IBM.Application.Interfaces;
using IBM.Core.DTO;
using IBM.Core.Entities;
using IBM.Core.Interfaces;
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
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService services;
        private readonly ILogger<TransactionsController> log;
        public TransactionsController(ITransactionService service, ILogger<TransactionsController> log)
        {
            this.services = service;
            this.log = log;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionResponse>>> GetAsync()
        {
            log.LogInformation("Iniciando consulta");
            var result = await services.GetTransactionsAsync();
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
        public async Task<ActionResult<IEnumerable<TransactionResponse>>> GetOfflineAsync()
        {
            log.LogInformation("Iniciando consulta");
            var result = await services.GetTransactionsAsync(true);
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
