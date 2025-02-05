using MediatR;
using TaskManager.Application.Requests;
using TaskManager.Data.Repositories.Contracts;
using TaskManager.Models;

namespace TaskManager.Application.Handlers
{
    public class DeleteTaskRequestHandler(ITaskRepository taskRepository) : IRequestHandler<DeleteTaskRequest, bool>
    {
        private readonly ITaskRepository _taskRepository = taskRepository;
        public async Task<bool> Handle(DeleteTaskRequest request, CancellationToken cancellationToken)
        {
            TaskItem? task = await _taskRepository.GetTaskByCodeAsync(request.TaskCode, cancellationToken);
            if (task is null)
            {
                return false;
            }

            return await _taskRepository.DeleteTaskAsync(task, cancellationToken);
        }
    }
}
