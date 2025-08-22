using GameAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace GameAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController<T> : ControllerBase where T : class
    {
        private readonly BaseService<T> _service;

        public BaseController(BaseService<T> service)
        {
            _service = service;
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAll() =>
            Ok(await _service.GetAllAsync());

        [HttpGet("GetAllBySort")]
        public virtual async Task<IActionResult> GetAllBySort(
            [FromQuery] string? sortBy = null,
            [FromQuery] string order = "asc")
        {
            var data = await _service.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                var prop = typeof(T).GetProperty(sortBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (prop != null)
                {
                    data = order.ToLower() == "desc"
                        ? data.OrderByDescending(x => prop.GetValue(x, null)).ToList()
                        : data.OrderBy(x => prop.GetValue(x, null)).ToList();
                }
            }

            return Ok(data);
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetById(int id)
        {
            var entity = await _service.GetByIdAsync(id);
            return entity != null ? Ok(entity) : NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Company")]
        public virtual async Task<IActionResult> Create(T entity)
        {
            var created = await _service.AddAsync(entity);

            // Dynamically pick "Id" property if exists
            var idProp = typeof(T).GetProperty("Id");
            var idValue = idProp?.GetValue(created);

            return CreatedAtAction(nameof(GetById), new { id = idValue }, created);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Company")]
        public virtual async Task<IActionResult> Update(int id, T entity)
        {
            var idProp = typeof(T).GetProperty("Id");
            if (idProp == null || (int)idProp.GetValue(entity)! != id)
                return BadRequest("Id mismatch");

            var updated = await _service.UpdateAsync(entity);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Company")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }

        [HttpGet("sort")]
        public virtual async Task<IActionResult> Sort([FromQuery] string sortBy, [FromQuery] bool descending = false)
        {
            var prop = typeof(T).GetProperty(sortBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (prop == null) return BadRequest($"Invalid sort field: {sortBy}");

            // Build key selector dynamically
            var result = await _service.SortAsync(x => prop.GetValue(x, null)!, descending);
            return Ok(result);
        }


        [HttpGet("count")]
        public virtual async Task<IActionResult> Count()
        {
            var count = await _service.CountAsync();
            return Ok(count);
        }
    }
}
