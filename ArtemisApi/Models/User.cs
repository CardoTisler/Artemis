using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtemisApi.Models;

public class User
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(50)]
    public string Username { get; set; }

    [Required]
    public string PasswordHash { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; }
    
    [MaxLength(50)]
    public string? FirstName { get; set; }
    
    [MaxLength(50)]
    public string? LastName { get; set; }
    
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreatedAt { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
    
    // Navigation properties
    public ICollection<UserBook> UserBooks { get; set; } = new List<UserBook>();
    public ICollection<ReadingSession> ReadingSessions { get; set; } = new List<ReadingSession>();
    public ICollection<ChapterNote> ChapterNotes { get; set; } = new List<ChapterNote>();
}