using System.ComponentModel.DataAnnotations;

namespace Crud.Entity
{
    public class Semester
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        public ICollection<Student>? Students { get; set; }
        public ICollection<Semester_Subject>? SemesterSubjects { get; set; }
    }
}
