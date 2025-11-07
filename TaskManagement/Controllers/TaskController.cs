using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Interfaces;
using static TaskManagement.Application.DTOs.TaskDTOs;

namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {

        private readonly ITaskService _svc;
        public TaskController(ITaskService svc) => _svc = svc;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskReadDto>>> Get() =>
        Ok(await _svc.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskReadDto>> Get(int id)
        {
            var t = await _svc.GetByIdAsync(id);
            if (t == null) return NotFound();
            return Ok(t);
        }

        // new endpoint for filtering and sorting with [Query Params]
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<TaskReadDto>>> GetFiltered(
            [FromQuery] bool? isCompleted,
            [FromQuery] string? sortBy = "CreatedAt")
        {
            var tasks = await _svc.GetFilteredAsync(isCompleted, sortBy);
            return Ok(tasks);
        }


        [HttpPost]
        public async Task<ActionResult<TaskReadDto>> Create([FromBody] TaskCreateDto dto)
        {
            var created = await _svc.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TaskUpdateDto dto)
        {
            await _svc.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _svc.DeleteAsync(id);
            return NoContent();
        }

    }
}
