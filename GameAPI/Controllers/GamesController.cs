using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameAPI.Data;
using GameAPI.Models;
using GameAPI.Services;

namespace GameAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly BaseService<Game> _context;

        public GamesController(BaseService<Game> context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Game>> GetGames()
        {
            return await _context.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGame(int id)
        {
            var game = await _context.GetByIdAsync(id);

            if (game == null)
            {
                return NotFound();
            }

            return game;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(int id, Game game)
        {
            if (id != game.GameId)
            {
                return BadRequest();
            }

            await _context.UpdateAsync(game);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Game>> PostGame(Game game)
        {
            await _context.AddAsync(game);

            return CreatedAtAction("GetGame", new { id = game.GameId }, game);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            await _context.DeleteAsync(id);

            return NoContent();
        }
    }
}
