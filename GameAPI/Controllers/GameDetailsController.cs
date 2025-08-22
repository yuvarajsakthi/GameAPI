using GameAPI.Models;
using GameAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameDetailsController : BaseController<GameDetail>
    {
        private readonly GameDetailService _service;

        public GameDetailsController(GameDetailService service) : base(service) 
        {
            _service = service;
        }

        [HttpGet("released-after/{date}")]
        public async Task<IActionResult> GetReleasedAfter(DateTime date)
        {
            var details = await _service.GetReleasedAfterAsync(date);
            return Ok(details);
        }

        [HttpGet("genre/{genre}")]
        public async Task<IActionResult> GetByGenre(string genre)
        {
            var details = await _service.GetByGenreAsync(genre);
            return Ok(details);
        }
    }
}
