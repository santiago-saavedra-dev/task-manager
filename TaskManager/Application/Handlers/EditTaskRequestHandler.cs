using MediatR;
using TaskManager.Application.Requests;
using TaskManager.Data.Repositories.Contracts;
using TaskManager.Models;
using TaskManager.ViewModels;

namespace TaskManager.Application.Handlers
{
    public class EditTaskRequestHandler(ITaskRepository repository) : IRequestHandler<EditTaskRequest, TaskVm?>
    {
        private readonly ITaskRepository _taskRepository = repository;
        public async Task<TaskVm?> Handle(EditTaskRequest request, CancellationToken cancellationToken)
        {
            TaskItem? task = await _taskRepository.GetTaskByCodeAsync(request.TaskCode, cancellationToken);
            if (task is null) return null;

            if (request.Title is not null)
            {
                task.Title = request.Title;
            }
            if (request.Description is not null)
            {
                task.Description = request.Description;
            }
            if (request.IsCompleted is not null)
            {
                task.IsCompleted = (bool)request.IsCompleted;
            }
            task.UpdatedAt = DateTime.UtcNow;

            TaskItem modifiedTask = await _taskRepository.EditTaskAsync(task, cancellationToken);
            return new TaskVm()
            {
                TaskCode = modifiedTask.TaskCode,
                Title = modifiedTask.Title,
                Description = modifiedTask.Description,
                IsCompleted = modifiedTask.IsCompleted,
                CreatedAt = modifiedTask.CreatedAt,
                UpdatedAt = modifiedTask.UpdatedAt,
            };
        }
    }
}
