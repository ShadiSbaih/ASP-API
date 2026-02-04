using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Database;

public class AppDbConext : DbContext
{
    public AppDbConext(DbContextOptions<AppDbConext> options) : base(options)
    {
    }

    public DbSet<Model.Product> Products { get; set; }

    public DbSet<Model.Category> Categories { get; set; }

    public DbSet<Model.Student> Students { get; set; }

    public DbSet<Model.Course> Courses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Many-to-Many relationship between Student and Course
        // EF Core 5.0+ automatically creates a join table for many-to-many relationships
        modelBuilder.Entity<Model.Student>()
            .HasMany(s => s.Courses)
            .WithMany(c => c.Students)
            .UsingEntity(j => j.ToTable("StudentCourses")); // Optional: specify join table name
    }
}
