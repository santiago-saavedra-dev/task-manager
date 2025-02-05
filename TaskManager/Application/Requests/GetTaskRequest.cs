using MediatR;
using TaskManager.ViewModels;

namespace TaskManager.Application.Requests
{
    public class GetTaskRequest(string taskCode) : IRequest<TaskVm?>
    {
        public string TaskCode { get; set; } = taskCode;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
