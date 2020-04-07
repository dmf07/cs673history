using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace cs673history.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetHistory()
        {
            await Task.Delay(10);
            return Ok("foo");
        }
    }
}