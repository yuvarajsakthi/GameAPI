using AutoMapper;
using GameAPI.DTOs;
using GameAPI.Models;
using GameAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameDetailsController : ControllerBase
    {
        private readonly GameApiService<GameDetail> _context;
        private readonly IMapper _mapper;

        public GameDetailsController(GameApiService<GameDetail> context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<GameDetailDto>> GetGameDetails()
        {
            var details = await _context.GetAllAsync();
            return _mapper.Map<IEnumerable<GameDetailDto>>(details);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GameDetailDto>> GetGameDetail(int id)
        {
            var detail = await _context.GetByIdAsync(id);

            if (detail == null) return NotFound();

            return _mapper.Map<GameDetailDto>(detail);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutGameDetail(int id, GameDetailDto dto)
        {
            var detail = _mapper.Map<GameDetail>(dto);
            detail.GameDetailId = id;

            await _context.UpdateAsync(detail);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<GameDetailDto>> PostGameDetail(GameDetailDto dto)
        {
            var detail = _mapper.Map<GameDetail>(dto);
            await _context.AddAsync(detail);

            var resultDto = _mapper.Map<GameDetailDto>(detail);

            return CreatedAtAction("GetGameDetail", new { id = detail.GameDetailId }, resultDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGameDetail(int id)
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
