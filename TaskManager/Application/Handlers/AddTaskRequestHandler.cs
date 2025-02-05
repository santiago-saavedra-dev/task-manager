using MediatR;
using TaskManager.Application.Requests;
using TaskManager.Data.Repositories.Contracts;
using TaskManager.Models;
using TaskManager.RequestBody;
using TaskManager.ViewModels;

namespace TaskManager.Application.Handlers
{
    public class AddTaskRequestHandler(ITaskRepository taskRepository) : IRequestHandler<AddTaskRequest, TaskVm>
    {
        private readonly ITaskRepository _taskRepository = taskRepository;
        public async Task<TaskVm> Handle(AddTaskRequest request, CancellationToken cancellationToken)
        {
            AddTaskRequestBody addTaskVm = request.Vm;

            TaskItem newTask = new()
            {
                TaskCode = Guid.NewGuid().ToString(),
                Title = addTaskVm.Title,
                Description = addTaskVm.Description,
                IsCompleted = addTaskVm.IsCompleted,
                CreatedAt = DateTime.UtcNow,
            };

            TaskItem response = await _taskRepository.AddTaskAsync(newTask, cancellationToken);
            return new TaskVm()
            {
                TaskCode = response.TaskCode,
                Title = response.Title,
                Description = response.Description,
                IsCompleted = response.IsCompleted,
                CreatedAt = response.CreatedAt,
            };
        }
    }
}
