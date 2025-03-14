using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtemisApi.Models;

public class Book
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(255)]
    public string Title { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    public string Author { get; set; } = null!;

    [MaxLength(1000)]
    public string? Description { get; set; }

    [MaxLength(500)]
    public string? CoverImageUrl { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Page count must be greater than zero.")]
    public int? PageCount { get; set; }

    [Required]
    [MaxLength(50)]
    public string ExternalId { get; set; }

    [DataType(DataType.Date)]
    public DateTime? PublishedDate { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreatedAt { get; set; }
    
    // Navigation Properties
    public ICollection<UserBook> UserBooks { get; set; } = new List<UserBook>();
    public ICollection<ReadingSession> ReadingSessions { get; set; } = new List<ReadingSession>();
    public ICollection<ChapterNote> ChapterNotes { get; set; } = new List<ChapterNote>();
}