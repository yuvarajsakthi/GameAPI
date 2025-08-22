using GameAPI.Models;
using GameAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : BaseController<Publisher>
    {
        private readonly PublisherService _service;

        public PublishersController(PublisherService service) : base(service)
        {
            _service = service;
        }

        [HttpGet("country/{country}")]
        public async Task<IActionResult> GetByCountry(string country)
        {
            var publishers = await _service.GetByCountryAsync(country);
            return Ok(publishers);
        }
    }
}
