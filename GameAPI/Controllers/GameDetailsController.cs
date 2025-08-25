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
    public class GameDetailsController : ControllerBase
    {
        private readonly BaseService<GameDetail> _context;

        public GameDetailsController(BaseService<GameDetail> context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<GameDetail>> GetGameDetails()
        {
            return await _context.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GameDetail>> GetGameDetail(int id)
        {
            var gameDetail = await _context.GetByIdAsync(id);

            if (gameDetail == null)
            {
                return NotFound();
            }

            return gameDetail;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutGameDetail(int id, GameDetail gameDetail)
        {
            if (id != gameDetail.GameDetailId)
            {
                return BadRequest();
            }

            await _context.UpdateAsync(gameDetail);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<GameDetail>> PostGameDetail(GameDetail gameDetail)
        {
           
            await _context.AddAsync(gameDetail);

            return CreatedAtAction("GetGameDetail", new { id = gameDetail.GameDetailId }, gameDetail);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGameDetail(int id)
        {
            await _context.DeleteAsync(id);

            return NoContent();
        }

    }
}
