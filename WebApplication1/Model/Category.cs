using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Model;

/// <summary>
/// Defines the <see cref="Category" />
/// </summary>
public class Category
{
    /// <summary>
    /// Gets or sets the Id
    /// </summary>
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the Name
    /// </summary>
    /// 
    [Required]
    [MaxLength(100)]
    [MinLength(3)]
    [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "Name can only contain letters, numbers, and spaces.")]
    public string Name { get; set; }

    public ICollection<Product> Products { get; set; }
}
