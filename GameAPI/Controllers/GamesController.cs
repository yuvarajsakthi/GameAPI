using GameAPI.Models;
using GameAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : BaseController<Game>
    {
        private readonly GameService _gameService;

        public GamesController(GameService gameService) : base(gameService)
        {
            _gameService = gameService;
        }

        [HttpGet("company/{companyId}")]
        [Authorize(Roles = "Admin,Company")]
        public async Task<IActionResult> GetByCompany(int companyId)
        {
            var games = await _gameService.GetByCompanyAsync(companyId);
            return Ok(games);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Company")]
        public async Task<IActionResult> AddGame([FromBody] Game game)
        {
            // You can verify that the UserId matches the current user for company users
            var added = await _gameService.AddGameAsync(game);
            return Ok(added);
        }
    }
}
