using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OcelotGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppropriateRecipesController : ControllerBase
    {
        [HttpGet("forweather")]
        public IActionResult GetForWeather([FromQuery]string weather)
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }
    }
}
