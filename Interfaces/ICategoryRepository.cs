using TaskManagement.Models;

namespace TaskManagement.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryEntity>> GetAllAsync();
        Task<CategoryEntity?> GetByIdAsync(int id);
        Task<CategoryEntity> AddAsync(CategoryEntity category);
    }
}
