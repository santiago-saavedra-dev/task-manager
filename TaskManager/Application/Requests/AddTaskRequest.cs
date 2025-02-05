using MediatR;
using TaskManager.RequestBody;
using TaskManager.ViewModels;

namespace TaskManager.Application.Requests
{
    public class AddTaskRequest(AddTaskRequestBody vm) : IRequest<TaskVm>
    {
        public AddTaskRequestBody Vm { get; set; } = vm;
    }
}
