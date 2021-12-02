using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IBM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestErrorController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            throw new System.Exception("Esto es una prueba de una excepcion manual");
        }

       
    }
}
