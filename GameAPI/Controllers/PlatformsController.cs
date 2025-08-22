using GameAPI.Models;
using GameAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : BaseController<Platform>
    {
        private readonly PlatformService _service;

        public PlatformsController(PlatformService service) : base(service)
        {
            _service = service;
        }

        [HttpGet("type/{type}")]
        public async Task<IActionResult> GetByType(string type)
        {
            var platforms = await _service.GetByTypeAsync(type);
            return Ok(platforms);
        }
    }
}
