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
    public class GameCompaniesController : ControllerBase
    {
        private readonly BaseService<GameCompany> _context;

        public GameCompaniesController(BaseService<GameCompany> context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<GameCompany>> GetGameCompanies()
        {
            return await _context.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GameCompany>> GetGameCompany(int id)
        {
            var gameCompany = await _context.GetByIdAsync(id);

            if (gameCompany == null)
            {
                return NotFound();
            }

            return gameCompany;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutGameCompany(int id, GameCompany gameCompany)
        {
            if (id != gameCompany.GameCompanyId)
            {
                return BadRequest();
            }

            await _context.UpdateAsync(gameCompany);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<GameCompany>> PostGameCompany(GameCompany gameCompany)
        {
            await _context.AddAsync(gameCompany);

            return CreatedAtAction("GetGameCompany", new { id = gameCompany.GameCompanyId }, gameCompany);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGameCompany(int id)
        {
            await _context.DeleteAsync(id);

            return NoContent();
        }
    }
}
