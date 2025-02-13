using ArtemisApi.Data;
using ArtemisApi.Interfaces;
using Common.Models;

namespace ArtemisApi.Repositories;

public class TodoRepository(DataContext context) : ITodoRepository
{
    public ICollection<Todo> GetTodos()
    {
        return context.Todos.ToList();
    }

    public Todo? GetTodoById(int todoId)
    {
        return context.Todos.Find(todoId);
    }

    public ICollection<Todo> GetTodosByUserId(int userId)
    {
        return context.Todos.Where(todo => todo.UserId == userId).ToList();
    }

    public bool AddTodo(Todo todo)
    {
        context.Add(todo);
        return Save();
    }

    public bool DeleteTodoById(int todoId)
    {
        var todo = GetTodoById(todoId);
        
        if (todo == null) return true;
        
        context.Todos.Remove(todo);
        return Save();
    }

    private bool Save() => context.SaveChanges() > 0;
}