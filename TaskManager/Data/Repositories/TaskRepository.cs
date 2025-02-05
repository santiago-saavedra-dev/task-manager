using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TaskManager.Data.Repositories.Contracts;
using TaskManager.Models;

namespace TaskManager.Data.Repositories
{
    public class TaskRepository(TaskDbContext context) : ITaskRepository
    {
        private readonly TaskDbContext _context = context;

        public async Task<TaskItem?> GetTaskByIdAsync(int id)
        {
            return await _context.TaskItems.FindAsync(id);
        }

        public async Task<TaskItem?> GetTaskByCodeAsync(string taskCode, CancellationToken cancellationToken)
        {
            return await _context.TaskItems.FirstOrDefaultAsync(x => x.TaskCode == taskCode, cancellationToken);
        }

        public async Task<IEnumerable<TaskItem>> GetTaskByPaginatedConditionAsync(Expression<Func<TaskItem, bool>> predicate, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            return await _context.TaskItems
                .AsNoTracking()
                .Where(predicate)
                .OrderBy(t => t.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }
        public async Task<IEnumerable<TaskItem>> GetTaskByConditionAsync(Expression<Func<TaskItem, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _context.TaskItems
                .AsNoTracking()
                .Where(predicate)
                .OrderBy(t => t.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<TaskItem> AddTaskAsync(TaskItem task, CancellationToken cancellationToken)
        {
            await _context.TaskItems.AddAsync(task, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return task;
        }

        public async Task<TaskItem> EditTaskAsync(TaskItem task, CancellationToken cancellationToken)
        {
            _context.Entry(task).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
            return task;
        }

        public async Task<bool> DeleteTaskAsync(TaskItem task, CancellationToken cancellationToken)
        {
            _context.TaskItems.Remove(task);
            int entitiesModified = await _context.SaveChangesAsync(cancellationToken);
            if (entitiesModified <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
