using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using cs673history.Controllers.Models;
using cs673history.Repository;
using cs673history.Repository.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HistoryItem = cs673history.Controllers.Models.HistoryItem;

namespace cs673history.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HistoryController : ControllerBase
    {
        private readonly IHistoryRepository _historyRepository;

        public HistoryController(IHistoryRepository historyRepository)
        {
            _historyRepository = historyRepository;
        }

        [HttpPost]
        public async Task<IActionResult> SaveHistory([FromBody] HistoryItem historyItem)
        {
            await _historyRepository.SaveHistory(new Repository.Models.HistoryItem
            {
                Date = DateTimeOffset.UtcNow,
                Title = historyItem.Title,
                Upc = historyItem.Upc,
                User = User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Email).Value
            });
            return Accepted();
        }

        [HttpGet]
        public async Task<IActionResult> GetHistory([FromQuery]PageRequest pageRequest)
        {
            return Ok(await _historyRepository.GetHistory(new HistoryQuery
            {
                Skip = pageRequest.Skip,
                Take = pageRequest.Take,
                User = User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Email).Value
            }));
        }
    }
}