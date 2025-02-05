using MediatR;
using TaskManager.ViewModels;

namespace TaskManager.Application.Requests
{
    public class EditTaskRequest(string taskCode, string? title = null, string? description = null, bool? isCompleted = null) : IRequest<TaskVm?>
    {
        public string TaskCode { get; set; } = taskCode;
        public string? Title { get; set; } = title;
        public string? Description { get; set; } = description;
        public bool? IsCompleted { get; set; } = isCompleted;
    }
}
