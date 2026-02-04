using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Database;
using WebApplication1.Model;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourseController : ControllerBase
{
    private readonly AppDbConext _context;

    public CourseController(AppDbConext context)
    {
        _context = context;
    }

    // GET: api/Course
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
    {
        return await _context.Courses
            .Include(c => c.Students)
            .ToListAsync();
    }

    // GET: api/Course/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Course>> GetCourse(Guid id)
    {
        var course = await _context.Courses
            .Include(c => c.Students)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (course == null)
        {
            return NotFound();
        }

        return course;
    }

    // POST: api/Course
    [HttpPost]
    public async Task<ActionResult<Course>> CreateCourse(Course course)
    {
        _context.Courses.Add(course);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, course);
    }

    // GET: api/Course/{id}/students
    [HttpGet("{id}/students")]
    public async Task<ActionResult<IEnumerable<Student>>> GetCourseStudents(Guid id)
    {
        var course = await _context.Courses
            .Include(c => c.Students)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (course == null)
        {
            return NotFound("Course not found");
        }

        return Ok(course.Students);
    }

    // PUT: api/Course/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCourse(Guid id, Course course)
    {
        if (id != course.Id)
        {
            return BadRequest();
        }

        _context.Entry(course).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CourseExists(id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }

    // DELETE: api/Course/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse(Guid id)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course == null)
        {
            return NotFound();
        }

        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CourseExists(Guid id)
    {
        return _context.Courses.Any(e => e.Id == id);
    }
}
