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
    public class PlatformsController : ControllerBase
    {
        private readonly BaseService<Platform> _context;

        public PlatformsController(BaseService<Platform> context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Platform>> GetPlatforms()
        {
            return await _context.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Platform>> GetPlatform(int id)
        {
            var platform = await _context.GetByIdAsync(id);

            if (platform == null)
            {
                return NotFound();
            }

            return platform;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlatform(int id, Platform platform)
        {
            if (id != platform.PlatformId)
            {
                return BadRequest();
            }

            await _context.UpdateAsync(platform);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Platform>> PostPlatform(Platform platform)
        {
            await _context.AddAsync(platform);

            return CreatedAtAction("GetPlatform", new { id = platform.PlatformId }, platform);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlatform(int id)
        {
            await _context.DeleteAsync(id);

            return NoContent();
        }
    }
}
