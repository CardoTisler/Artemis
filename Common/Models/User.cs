using System.ComponentModel.DataAnnotations;

namespace Common.Models;

public class User
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(50, MinimumLength = 5)]
    [EmailAddress]
    public required string Email { get; set; }
    
    [Required]
    public required string Password { get; set; }

    // navigation property
    public ICollection<Todo> Todos { get; set; } = new List<Todo>();
}