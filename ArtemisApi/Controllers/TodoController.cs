using ArtemisApi.Interfaces;
using ArtemisApi.Services;
using Common.Dto;
using Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArtemisApi.Controllers;

[Route("todo")]
[ApiController]
[Authorize]
public class TodoController : Controller
{
    private readonly ITodoRepository _repository;
    private readonly KafkaProducerService _producer;


    public TodoController(ITodoRepository repository, KafkaProducerService producer)
    {
        _repository = repository;
        _producer = producer;
    }
    
    [HttpGet("all", Name = "GetAllTodos")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Todo>))]
    public IActionResult GetTodos()
    {
        var todos = _repository.GetTodos();
        
        return Ok(todos);
    }

    [HttpGet("{id}", Name = "GetTodoById")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Todo))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetTodoById(int todoId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var todo = _repository.GetTodoById(todoId);
        if (todo == null) return NotFound();

        return Ok(todo);
    }

    [HttpPost(Name = "AddTodo")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult AddTodo(TodoDto payload)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var todo = new Todo
        {
            Title = payload.Title,
            Description = payload.Description,
            IsCompleted = payload.IsCompleted,
            CreatedAt = DateTime.Now,
            UserId = payload.UserId
        };
        
        var added = _repository.AddTodo(todo);
        if (added == false) return BadRequest();

        // _producer.SendMessageAsync(new { todo.Id, todo.Title, todo.IsCompleted });
        
        return Ok(new { Success = true });
    }

    [HttpDelete("{id}", Name = "DeleteTodo")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult DeleteTodo(int id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var deleted = _repository.DeleteTodoById(id);
        if (deleted == false) return BadRequest();
        
        return NoContent();
    }
}