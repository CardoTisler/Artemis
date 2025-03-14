using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtemisApi.Models;

public class ChapterNote
{
    [Required]
    public Guid Id { get; set; }
    
    [Required]
    public Guid UserId { get; set; }
    
    [Required]
    public Guid BookId { get; set; }
    
    [Required]
    [Range(1, int.MaxValue)]
    public int ChapterNumber { get; set; }
    
    [MaxLength(1000)]
    public string Content { get; set; }
    
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreatedAt { get; set; }
    
    // Navigation properties
    public User User { get; set; }
    public Book Book { get; set; }
}