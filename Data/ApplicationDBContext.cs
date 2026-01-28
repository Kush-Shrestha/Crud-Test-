using Crud.Entity;
using Microsoft.EntityFrameworkCore;

namespace Crud.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext>options): base(options)
        {
          
        }
        public DbSet<Semester> Semester { get; set; }//to create tables in the sql
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }

    }
}
