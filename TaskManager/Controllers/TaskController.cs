using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TaskManager.Application.Requests;
using TaskManager.RequestBody;
using TaskManager.ViewModels;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TaskController(ISender sender) : ControllerBase
    {
        private readonly ISender _sender = sender;

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> GetTaskItems()
        {
            try
            {
                GetAllTasksRequest request = new();
                IEnumerable<TaskVm> response = await _sender.Send(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost("get")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTask([FromBody] GetTaskRequestBody request)
        {
            try
            {
                GetTaskRequest requestGetTask = new(request.TaskCode);
                TaskVm? response = await _sender.Send(requestGetTask);
                if (response == null)
                {
                    return NotFound($"Task '{requestGetTask.TaskCode}' was not found");
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost("add")]
        [AllowAnonymous]
        public async Task<IActionResult> AddTask([FromBody] AddTaskRequestBody request)
        {
            try
            {
                AddTaskRequest requestAddTask = new(request);
                TaskVm? response = await _sender.Send(requestAddTask);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost("edit")]
        [AllowAnonymous]
        public async Task<IActionResult> EditTask([FromBody] EditTaskRequestBody request)
        {
            try
            {
                if (request.Title is null && request.Description is null && request.IsCompleted is null)
                {
                    return BadRequest("All fields where empty, change at least one field");
                }

                EditTaskRequest requestEditTask = new(request.TaskCode, request.Title, request.Description, request.IsCompleted);
                TaskVm? response = await _sender.Send(requestEditTask);
                if (response is null)
                {
                    return NotFound($"Task '{request.TaskCode}' was not found");
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost("delete")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteTask([FromBody] DeleteTaskRequestBody request)
        {
            try
            {
                DeleteTaskRequest requestDeleteTask = new(request.TaskCode);
                bool response = await _sender.Send(requestDeleteTask);
                if (!response)
                {
                    return NotFound($"Task '{request.TaskCode}' was not found, so it couldn't be deleted");
                }

                return Ok("Task deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
