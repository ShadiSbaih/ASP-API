using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Database;
using WebApplication1.Model;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private readonly AppDbConext _context;

    public StudentController(AppDbConext context)
    {
        _context = context;
    }

    // GET: api/Student
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
    {
        return await _context.Students
            .Include(s => s.Courses)
            .ToListAsync();
    }

    // GET: api/Student/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Student>> GetStudent(Guid id)
    {
        var student = await _context.Students
            .Include(s => s.Courses)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (student == null)
        {
            return NotFound();
        }

        return student;
    }

    // POST: api/Student
    [HttpPost]
    public async Task<ActionResult<Student>> CreateStudent(Student student)
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
    }

    // POST: api/Student/{studentId}/enroll/{courseId}
    [HttpPost("{studentId}/enroll/{courseId}")]
    public async Task<IActionResult> EnrollInCourse(Guid studentId, Guid courseId)
    {
        var student = await _context.Students
            .Include(s => s.Courses)
            .FirstOrDefaultAsync(s => s.Id == studentId);

        if (student == null)
        {
            return NotFound("Student not found");
        }

        var course = await _context.Courses.FindAsync(courseId);

        if (course == null)
        {
            return NotFound("Course not found");
        }

        if (student.Courses.Any(c => c.Id == courseId))
        {
            return BadRequest("Student is already enrolled in this course");
        }

        student.Courses.Add(course);
        await _context.SaveChangesAsync();

        return Ok($"Student {student.FirstName} {student.LastName} enrolled in {course.CourseName}");
    }

    // DELETE: api/Student/{studentId}/unenroll/{courseId}
    [HttpDelete("{studentId}/unenroll/{courseId}")]
    public async Task<IActionResult> UnenrollFromCourse(Guid studentId, Guid courseId)
    {
        var student = await _context.Students
            .Include(s => s.Courses)
            .FirstOrDefaultAsync(s => s.Id == studentId);

        if (student == null)
        {
            return NotFound("Student not found");
        }

        var course = student.Courses.FirstOrDefault(c => c.Id == courseId);

        if (course == null)
        {
            return NotFound("Student is not enrolled in this course");
        }

        student.Courses.Remove(course);
        await _context.SaveChangesAsync();

        return Ok($"Student {student.FirstName} {student.LastName} unenrolled from {course.CourseName}");
    }

    // PUT: api/Student/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStudent(Guid id, Student student)
    {
        if (id != student.Id)
        {
            return BadRequest();
        }

        _context.Entry(student).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!StudentExists(id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }

    // DELETE: api/Student/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(Guid id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null)
        {
            return NotFound();
        }

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool StudentExists(Guid id)
    {
        return _context.Students.Any(e => e.Id == id);
    }
}
