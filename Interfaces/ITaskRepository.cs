using TaskManagement.Models;

namespace TaskManagement.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskEntity>> GetAllAsync();
        Task<TaskEntity?> GetByIdAsync(int id);
        Task<TaskEntity> AddAsync(TaskEntity entity);
        Task<TaskEntity> UpdateAsync(TaskEntity entity);
        Task<bool> DeleteAsync(int id);
    }
}
