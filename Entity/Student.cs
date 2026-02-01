using System.ComponentModel.DataAnnotations.Schema;

namespace Crud.Entity
{
    public class Student
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        
        [ForeignKey("Semester")]
        public int? SemesterId { get; set; }

        public Semester? Semester { get; set; }
    }
}
