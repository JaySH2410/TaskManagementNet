using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskManagement.DTOs;
using TaskManagement.Interfaces;

namespace TaskManagement.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return Ok(ApiResponse.Success(tasks, "Tasks retrieved successfully"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
                return NotFound();

            return Ok(ApiResponse.Success(task, "Task retrieved successfully"));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskDto dto)
        {
            var created = await _taskService.CreateTaskAsync(dto);
            return CreatedAtAction(nameof(GetById),
             new { id = created.Id },
             ApiResponse.Success(created, "Task created successfully"));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateTaskDto dto)
        {
            var updated = await _taskService.UpdateTaskAsync(id, dto);
            if (updated == null)
                return NotFound();

            return Ok(ApiResponse.Success(updated, "Task updated successfully"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _taskService.DeleteTaskAsync(id);
            if (!success)
                return NotFound();

            return Ok(ApiResponse.Success(null, "Task deleted successfully"));
        }
    }
}
