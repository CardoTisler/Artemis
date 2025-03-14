using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtemisApi.Models;

public class ReadingSession
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required]
    public Guid UserId { get; set; }
    
    [Required]
    public Guid BookId { get; set; }
    
    [Required]
    public int EndPageNumber { get; set; }
    
    [Required]
    public int DurationInSeconds { get; set; }
    
    [Required]
    public ICollection<string> Subjects { get; set; }
    
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime? SessionEndTimestamp { get; set; }
    
    // Navigation properties
    public User User { get; set; }
    public Book Book { get; set; }
}