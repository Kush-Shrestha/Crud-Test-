using Microsoft.EntityFrameworkCore;
using practicing.Domain.Entity;

namespace practicing.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options)
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<AssignSubject> Semester_Subjects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
