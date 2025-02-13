using Common.Models;

namespace ArtemisApi.Interfaces;

public interface ITodoRepository
{
    ICollection<Todo> GetTodos();
    Todo? GetTodoById(int todoId);
    ICollection<Todo> GetTodosByUserId(int userId);
    bool AddTodo(Todo todo);
    bool DeleteTodoById(int todoId);
}