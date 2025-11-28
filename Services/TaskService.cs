using TaskManagement.Models;
using TaskManagement.DTOs;
using TaskManagement.Interfaces;
using System.Runtime.InteropServices;

namespace TaskManagement.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ILogger<TaskService> _logger;
        //private readonly ICurrentUserService _currentUser;

        public TaskService(ITaskRepository taskRepository, ILogger<TaskService> logger)
        {
            _taskRepository = taskRepository;
            _logger = logger;
            //_currentUser = currentUser;
        }

        private TaskDto ToDto(TaskEntity entity)
        {
            return new TaskDto
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                Status = entity.Status,
                CategoryId = entity.CategoryId,
                CategoryName = entity.Category?.Name,
                CreatedAt = entity.CreatedAt
            };
        }

        private TaskEntity FromCreateDto(CreateTaskDto dto)
        {

            //if (_currentUser.UserId is null)
            //    throw new UnauthorizedAccessException();
            return new TaskEntity
            {
                Title = dto.Title,
                Description = dto.Description,
                Status = dto.Status,
                CategoryId = dto.CategoryId,
                CreatedAt = DateTime.Now
                //UserId = _currentUser.UserId.Value
            };
        }

        private void ApplyUpdate(TaskEntity entity, UpdateTaskDto dto)
        {
            entity.Title = dto.Title;
            entity.Description = dto.Description;
            entity.Status = dto.Status;
            entity.CategoryId = dto.CategoryId;
        }

        public async Task<IEnumerable<TaskDto>> GetAllTasksAsync()
        {
            try
            {
                //var userId = _currentUser.UserId ?? throw new UnauthorizedAccessException();

                _logger.LogInformation("Fetching all tasks");
                var tasks = await _taskRepository.GetAllAsync();
                return tasks.Select(ToDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching tasks");
                throw;
            }
        }

        public async Task<TaskDto?> GetTaskByIdAsync(int id)
        {
            try
            {

                _logger.LogInformation("Fetching task by Id = {Id}", id);
                var task = await _taskRepository.GetByIdAsync(id);

                return task == null ? null : ToDto(task);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching task {Id}", id);
                throw;
            }
        }

        public async Task<TaskDto> CreateTaskAsync(CreateTaskDto dto)
        {
            try
            {

                _logger.LogInformation("Creating task [{Title}]", dto.Title);
                var entity = FromCreateDto(dto);

                var saved = await _taskRepository.AddAsync(entity);
                _logger.LogInformation("Task created successfully with Id = {Id}", saved.Id);

                return ToDto(saved);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating task");
                throw;
            }
        }

        public async Task<TaskDto?> UpdateTaskAsync(int id, UpdateTaskDto dto)
        {
            try
            {

                _logger.LogInformation("Updating task {Id}", id);

                var existing = await _taskRepository.GetByIdAsync(id);
                if (existing == null)
                {
                    _logger.LogWarning("Task {Id} not found for update", id);
                    return null;
                }

                ApplyUpdate(existing, dto);
                var updated = await _taskRepository.UpdateAsync(existing);

                _logger.LogInformation("Task {Id} updated successfully", id);
                return ToDto(updated);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating task {Id}", id);
                throw;
            }
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            try
            {

                _logger.LogInformation("Deleting task {Id}", id);
                var success = await _taskRepository.DeleteAsync(id);

                if (!success)
                    _logger.LogWarning("Task {Id} not found for delete", id);

                return success;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting task {Id}", id);
                throw;
            }
        }
    }
}
