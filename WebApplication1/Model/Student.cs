using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Model;

/// <summary>
/// Represents a Student entity
/// </summary>
public class Student
{
    /// <summary>
    /// Gets or sets the unique identifier for the student
    /// </summary>
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the student's first name
    /// </summary>
    [Required]
    [MaxLength(100)]
    [MinLength(2)]
    public string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the student's last name
    /// </summary>
    [Required]
    [MaxLength(100)]
    [MinLength(2)]
    public string LastName { get; set; }

    /// <summary>
    /// Gets or sets the student's email address
    /// </summary>
    [Required]
    [EmailAddress]
    [MaxLength(255)]
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the enrollment date
    /// </summary>
    [Required]
    public DateTime EnrollmentDate { get; set; } = DateTime.Now;

    /// <summary>
    /// Navigation property: Collection of courses the student is enrolled in
    /// Many-to-Many relationship with Course
    /// </summary>
    public ICollection<Course> Courses { get; set; } = new List<Course>();
}
