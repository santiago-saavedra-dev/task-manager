using MediatR;

namespace TaskManager.Application.Requests
{
    public class DeleteTaskRequest(string taskCode) : IRequest<bool>
    {
        public string TaskCode { get; set; } = taskCode;
    }
}
