using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Model;

/// <summary>
/// Represents a Course entity
/// </summary>
public class Course
{
    /// <summary>
    /// Gets or sets the unique identifier for the course
    /// </summary>
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the course code (e.g., CS101, MATH201)
    /// </summary>
    [Required]
    [MaxLength(20)]
    public string CourseCode { get; set; }

    /// <summary>
    /// Gets or sets the course name
    /// </summary>
    [Required]
    [MaxLength(200)]
    [MinLength(3)]
    public string CourseName { get; set; }

    /// <summary>
    /// Gets or sets the course description
    /// </summary>
    [MaxLength(500)]
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the number of credits for the course
    /// </summary>
    [Required]
    [Range(1, 10, ErrorMessage = "Credits must be between 1 and 10.")]
    public int Credits { get; set; }

    /// <summary>
    /// Navigation property: Collection of students enrolled in the course
    /// Many-to-Many relationship with Student
    /// </summary>
    public ICollection<Student> Students { get; set; } = new List<Student>();
}
