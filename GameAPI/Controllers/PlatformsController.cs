using Microsoft.AspNetCore.Mvc;
using GameAPI.Models;
using GameAPI.Services;
using GameAPI.DTOs;
using AutoMapper;

namespace GameAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly GameApiService<Platform> _context;
        private readonly IMapper _mapper;

        public PlatformsController(GameApiService<Platform> context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Platforms
        [HttpGet]
        public async Task<IEnumerable<PlatformDto>> GetPlatforms()
        {
            var platforms = await _context.GetAllAsync();
            return _mapper.Map<IEnumerable<PlatformDto>>(platforms);
        }

        // GET: api/Platforms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlatformDto>> GetPlatform(int id)
        {
            var platform = await _context.GetByIdAsync(id);

            if (platform == null)
            {
                return NotFound();
            }

            return _mapper.Map<PlatformDto>(platform);
        }

        // PUT: api/Platforms/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlatform(int id, PlatformDto platformDto)
        {
            var platform = _mapper.Map<Platform>(platformDto);
            platform.PlatformId = id; // ensure id consistency

            await _context.UpdateAsync(platform);

            return NoContent();
        }

        // POST: api/Platforms
        [HttpPost]
        public async Task<ActionResult<PlatformDto>> PostPlatform(PlatformDto platformDto)
        {
            var platform = _mapper.Map<Platform>(platformDto);
            await _context.AddAsync(platform);

            var resultDto = _mapper.Map<PlatformDto>(platform);
            return CreatedAtAction("GetPlatform", new { id = platform.PlatformId }, resultDto);
        }

        // DELETE: api/Platforms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlatform(int id)
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
