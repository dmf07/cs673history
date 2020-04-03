using Microsoft.AspNetCore.Mvc;

namespace cs673history.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AliveController : ControllerBase
    {
        [HttpGet]
        public IActionResult Alive()
        {
            return Ok("Alive");
        }
    }
}