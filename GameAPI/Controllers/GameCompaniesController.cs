using AutoMapper;
using GameAPI.DTOs;
using GameAPI.Models;
using GameAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameCompaniesController : ControllerBase
    {
        private readonly GameApiService<GameCompany> _context;
        private readonly IMapper _mapper;

        public GameCompaniesController(GameApiService<GameCompany> context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<GameCompanyDto>> GetGameCompanies()
        {
            var companies = await _context.GetAllAsync();
            return _mapper.Map<IEnumerable<GameCompanyDto>>(companies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GameCompanyDto>> GetGameCompany(int id)
        {
            var gameCompany = await _context.GetByIdAsync(id);

            if (gameCompany == null)
                return NotFound();

            return _mapper.Map<GameCompanyDto>(gameCompany);
        }

        [HttpPost]
        public async Task<ActionResult<GameCompanyDto>> PostGameCompany(GameCompanyDto gameCompanyDto)
        {
            var gameCompany = _mapper.Map<GameCompany>(gameCompanyDto);
            await _context.AddAsync(gameCompany);

            var resultDto = _mapper.Map<GameCompanyDto>(gameCompany);

            return CreatedAtAction(nameof(GetGameCompany), new { id = resultDto.GameCompanyId }, resultDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutGameCompany(int id, GameCompanyDto gameCompanyDto)
        {
            if (id != gameCompanyDto.GameCompanyId)
                return BadRequest();

            var gameCompany = _mapper.Map<GameCompany>(gameCompanyDto);

            await _context.UpdateAsync(gameCompany);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGameCompany(int id)
        {
            await _context.DeleteAsync(id);
            return NoContent();
        }
    }
}
