using MediatR;
using TaskManager.Application.Requests;
using TaskManager.Data.Repositories.Contracts;
using TaskManager.Models;
using TaskManager.ViewModels;

namespace TaskManager.Application.Handlers
{
    public class GetAllTasksRequestHandler(ITaskRepository taskRepository) : IRequestHandler<GetAllTasksRequest, IEnumerable<TaskVm>>
    {
        private readonly ITaskRepository _taskRepository = taskRepository;

        public async Task<IEnumerable<TaskVm>> Handle(GetAllTasksRequest request, CancellationToken cancellationToken)
        {
            IEnumerable<TaskItem> matches = await _taskRepository.GetTaskByPaginatedConditionAsync(x => true, request.PageNumber, request.PageSize, cancellationToken);
            ICollection<TaskVm> vms = [];

            foreach (TaskItem task in matches)
            {
                TaskVm vm = new()
                {
                    TaskCode = task.TaskCode,
                    Title = task.Title,
                    Description = task.Description,
                    IsCompleted = task.IsCompleted,
                    CreatedAt = task.CreatedAt,
                    UpdatedAt = task.UpdatedAt
                };
                vms.Add(vm);
            }

            return vms.AsEnumerable();
        }
    }
}
