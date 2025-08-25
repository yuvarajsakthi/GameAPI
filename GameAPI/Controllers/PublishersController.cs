using Microsoft.AspNetCore.Mvc;
using GameAPI.Models;
using GameAPI.Services;
using GameAPI.DTOs;
using AutoMapper;

namespace GameAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly GameApiService<Publisher> _context;
        private readonly IMapper _mapper;

        public PublishersController(GameApiService<Publisher> context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Publishers
        [HttpGet]
        public async Task<IEnumerable<PublisherDto>> GetPublishers()
        {
            var publishers = await _context.GetAllAsync();
            return _mapper.Map<IEnumerable<PublisherDto>>(publishers);
        }

        // GET: api/Publishers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PublisherDto>> GetPublisher(int id)
        {
            var publisher = await _context.GetByIdAsync(id);

            if (publisher == null)
            {
                return NotFound();
            }

            return _mapper.Map<PublisherDto>(publisher);
        }

        // PUT: api/Publishers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPublisher(int id, PublisherDto publisherDto)
        {
            var publisher = await _context.GetByIdAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }

            // map incoming DTO → existing entity
            _mapper.Map(publisherDto, publisher);
            await _context.UpdateAsync(publisher);

            return NoContent();
        }

        // POST: api/Publishers
        [HttpPost]
        public async Task<ActionResult<PublisherDto>> PostPublisher(PublisherDto publisherDto)
        {
            var publisher = _mapper.Map<Publisher>(publisherDto);
            await _context.AddAsync(publisher);

            return CreatedAtAction(nameof(GetPublisher),
                new { id = publisher.PublisherId },
                _mapper.Map<PublisherDto>(publisher));
        }

        // DELETE: api/Publishers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            var publisher = await _context.GetByIdAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }

            await _context.DeleteAsync(id);
            return NoContent();
        }
    }
}
