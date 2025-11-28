using Microsoft.EntityFrameworkCore;
using TaskManagement.Data;
using TaskManagement.Interfaces;
using TaskManagement.Models;

namespace TaskManagement.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskEntity>> GetAllAsync()
        {
            return await 
                _context.Tasks
                .Include(t => t.Category)
                //.Where(t => t.UserId == userId)
                .ToListAsync();
        }

        public async Task<TaskEntity?> GetByIdAsync(int id)
        {
            return await 
                _context.Tasks
                .Include(t =>t.Category)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<TaskEntity> AddAsync(TaskEntity entity)
        {
            _context.Tasks.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TaskEntity> UpdateAsync(TaskEntity entity)
        {
            _context.Tasks.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Tasks.FindAsync(id);
            if (existing == null)
                return false;

            _context.Tasks.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
