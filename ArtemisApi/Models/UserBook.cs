using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ArtemisApi.Enums;

namespace ArtemisApi.Models;

[Table("user_books")]
public class UserBook
{
    [Required]
    public Guid BookId { get; set; }
    
    [Required]
    public Guid UserId { get; set; }
    
    [Required]
    public SystemShelf ShelfId { get; set; }
    
    [Required]
    public DateTime CreatedAt { get; set; }
    
    public Book Book { get; set; }
    public User User { get; set; }
    public Shelf Shelf { get; set; }
}
