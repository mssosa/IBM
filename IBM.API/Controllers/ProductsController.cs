using IBM.Application.Interfaces;
using IBM.Core.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IBM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService services;
        private readonly ILogger<ProductsController> log;

        public ProductsController(IProductService service, ILogger<ProductsController> log)
        {
            this.services = service;
            this.log = log;
        }


        [HttpGet]
        public async Task<ActionResult<ProductOnlyResponse>> GetAsync()
        {
            log.LogInformation("Iniciando consulta");
            var result = await services.GetProductsAsync();
            if (!result.Any())
            {
                string message = "No hay elementos para mostrar";
                log.LogInformation(message);
                return NotFound(message);
            }

            log.LogInformation("Todo salio OK.");
            return Ok(result);
        }


        [HttpGet("{sku}")]
        public async Task<ActionResult<ProductResponse>> GetAsync([FromHeader] ProductRequest request)
        {
            log.LogInformation("Iniciando consulta");
            var result = await services.GetProductAsync(request);
            if (!result.transactions.Any())
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
