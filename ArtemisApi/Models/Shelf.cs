using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ArtemisApi.Enums;

namespace ArtemisApi.Models;

public class Shelf
{
    [Required]
    [Column(TypeName = "text")]
    public SystemShelf Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    // Navigation properties
    public ICollection<UserBook> UserBooks { get; set; } = new List<UserBook>();
}