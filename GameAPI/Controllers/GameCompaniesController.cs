using GameAPI.Models;
using GameAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameCompaniesController : BaseController<GameCompany>
    {
        private readonly GameCompanyService _service;

        public GameCompaniesController(GameCompanyService service) : base(service)
        {
            _service = service;
        }


        [HttpGet("year/{year}")]
        public async Task<IActionResult> GetByFoundedYear(int year)
        {
            var companies = await _service.GetByFoundedYearAsync(year);
            return Ok(companies);
        }

        [HttpGet("SearchByCompany")]
        public async Task<IActionResult> SearchGameCompaniesByName([FromQuery] string name)
        {
            var companies = await _service.SearchByNameAsync(name);
            return Ok(companies);
        }
    }
}
