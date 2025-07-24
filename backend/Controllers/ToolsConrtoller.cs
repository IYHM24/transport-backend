using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend_transport.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToolsConrtoller: ControllerBase
    {
        public ToolsConrtoller()
        {
            
        }

        [HttpGet]
        public IActionResult Get()
        {
            return new OkObjectResult("Hola mundo!");
        }

        [HttpGet]
        [Authorize]
        [Route("secure")]
        public IActionResult GetSecure()
        {
            return new OkObjectResult("Secure working!");
        }
    }
}
