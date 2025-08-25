using AutoMapper;
using GameAPI.DTOs;
using GameAPI.Models;
using GameAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly GameApiService<Game> _context;
        private readonly IMapper _mapper;

        public GamesController(GameApiService<Game> context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<GameDto>> GetGames()
        {
            var games = await _context.GetAllAsync();
            return _mapper.Map<IEnumerable<GameDto>>(games);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GameDto>> GetGame(int id)
        {
            var game = await _context.GetByIdAsync(id);

            if (game == null) return NotFound();

            return _mapper.Map<GameDto>(game);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(int id, GameDto gameDto)
        {
            var game = _mapper.Map<Game>(gameDto);
            game.GameId = id; // ensure correct Id

            await _context.UpdateAsync(game);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<GameDto>> PostGame(GameDto gameDto)
        {
            var game = _mapper.Map<Game>(gameDto);
            await _context.AddAsync(game);

            var resultDto = _mapper.Map<GameDto>(game);

            return CreatedAtAction("GetGame", new { id = game.GameId }, resultDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            await _context.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("count")]
        public async Task<ActionResult<int>> GetPublishersCount()
        {
            var count = await _context.CountAsync();
            return Ok(count);
        }

    }
}
