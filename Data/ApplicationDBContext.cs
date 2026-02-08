using Microsoft.EntityFrameworkCore;
using Crud.Entity;

namespace Crud.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        
        public DbSet<Student> Students { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Semester_Subject> Semester_Subjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Semester_Subject relationships
            modelBuilder.Entity<Semester_Subject>()
                .HasOne(ss => ss.Semester)
                .WithMany(s => s.SemesterSubjects)
                .HasForeignKey(ss => ss.SemesterId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Semester_Subject>()
                .HasOne(ss => ss.Subject)
                .WithMany(s => s.SemesterSubjects)
                .HasForeignKey(ss => ss.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Student-Semester relationship
            modelBuilder.Entity<Student>()
                .HasOne(st => st.Semester)
                .WithMany(s => s.Students)
                .HasForeignKey(st => st.SemesterId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
