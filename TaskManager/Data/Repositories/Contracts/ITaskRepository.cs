using System.Linq.Expressions;
using TaskManager.Models;

namespace TaskManager.Data.Repositories.Contracts
{
    public interface ITaskRepository
    {
        public Task<TaskItem?> GetTaskByIdAsync(int id);
        public Task<TaskItem?> GetTaskByCodeAsync(string taskCode, CancellationToken cancellationToken);
        public Task<IEnumerable<TaskItem>> GetTaskByPaginatedConditionAsync(Expression<Func<TaskItem, bool>> predicate, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
        public Task<IEnumerable<TaskItem>> GetTaskByConditionAsync(Expression<Func<TaskItem, bool>> predicate, CancellationToken cancellationToken = default);
        public Task<TaskItem> AddTaskAsync(TaskItem task, CancellationToken cancellationToken);
        public Task<TaskItem> EditTaskAsync(TaskItem task, CancellationToken cancellationToken);
        public Task<bool> DeleteTaskAsync(TaskItem task, CancellationToken cancellationToken);
    }
}
