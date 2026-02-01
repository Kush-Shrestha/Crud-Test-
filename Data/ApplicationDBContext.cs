using Crud.Entity;
using Microsoft.EntityFrameworkCore;

namespace Crud.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext>options): base(options)
        {
          
        }
        public DbSet<Semester> Semester { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<SemesterSubject> SemesterSubjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Semester)
                .WithMany()
                .HasForeignKey(s => s.Semester_id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SemesterSubject>()
                .HasOne(ss => ss.Semester)
                .WithMany()
                .HasForeignKey(ss => ss.Semester_id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SemesterSubject>()
                .HasOne(ss => ss.Subject)
                .WithMany()
                .HasForeignKey(ss => ss.Subject_id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
