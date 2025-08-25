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
    public class PublishersController : ControllerBase
    {
        private readonly BaseService<Publisher> _context;

        public PublishersController(BaseService<Publisher> context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Publisher>> GetPublishers()
        {
            return await _context.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Publisher>> GetPublisher(int id)
        {
            var publisher = await _context.GetByIdAsync(id);

            if (publisher == null)
            {
                return NotFound();
            }

            return publisher;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPublisher(int id, Publisher publisher)
        {
            if (id != publisher.PublisherId)
            {
                return BadRequest();
            }

            await _context.UpdateAsync(publisher);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Publisher>> PostPublisher(Publisher publisher)
        {
            await _context.AddAsync(publisher);

            return CreatedAtAction("GetPublisher", new { id = publisher.PublisherId }, publisher);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            await _context.DeleteAsync(id);

            return NoContent();
        }
    }
}
