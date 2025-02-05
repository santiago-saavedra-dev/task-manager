using MediatR;
using TaskManager.ViewModels;

namespace TaskManager.Application.Requests
{
    public class GetAllTasksRequest : IRequest<IEnumerable<TaskVm>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
