using System.ComponentModel.DataAnnotations;

namespace Common.Models;

public class Todo
{
    [Required]
    public required int Id { get; set; }
    
    [Required]
    [StringLength(100, MinimumLength = 1)]
    public required string Title { get; set; }
    
    [Required]
    [StringLength(280, MinimumLength = 1)]
    public required string Description { get; set; }
    
    [Required]
    [Range(0,1)]
    public required bool IsCompleted { get; set; }

    [Required]
    public required DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
    
    // FK
    public required int UserId { get; set; }
    
    // navigation property
    public required User User { get; set; }
}           