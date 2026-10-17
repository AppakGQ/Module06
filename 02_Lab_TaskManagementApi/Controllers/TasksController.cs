using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.Models;

namespace TaskManagementApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private static readonly List<TaskItem> Tasks = new()
    {
        new TaskItem { Id = 1, Title = "Изучить JWT",              Description = "Прочитать лекцию по модулю 06",         IsCompleted = true  },
        new TaskItem { Id = 2, Title = "Сделать лабораторную",     Description = "Реализовать Task Management API",       IsCompleted = false },
        new TaskItem { Id = 3, Title = "Подготовить скриншоты",    Description = "Токен, 401, успешный запрос, Claims",   IsCompleted = false }
    };
    private static int _nextId = 4;
    private static readonly object Lock = new();

    [HttpGet]
    public ActionResult<IEnumerable<TaskItem>> GetAll()
    {
        lock (Lock) return Ok(Tasks.ToList());
    }

    [HttpGet("{id:int}")]
    public ActionResult<TaskItem> GetById(int id)
    {
        lock (Lock)
        {
            var task = Tasks.FirstOrDefault(t => t.Id == id);
            return task is null ? NotFound(new { message = $"Task {id} not found" }) : Ok(task);
        }
    }

    [HttpPost]
    [Authorize]
    public IActionResult Create([FromBody] TaskItem task)
    {
        var userName = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "name")?.Value;

        lock (Lock)
        {
            task.Id = _nextId++;
            Tasks.Add(task);
        }

        return CreatedAtAction(nameof(GetById), new { id = task.Id }, new
        {
            message = "Task created",
            createdBy = userName,
            task
        });
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public IActionResult Delete(int id)
    {
        var userName = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "name")?.Value;

        lock (Lock)
        {
            var task = Tasks.FirstOrDefault(t => t.Id == id);
            if (task is null)
                return NotFound(new { message = $"Task {id} not found" });

            Tasks.Remove(task);
            return Ok(new { message = "Task deleted", deletedBy = userName, id });
        }
    }
}
