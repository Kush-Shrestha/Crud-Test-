using System.ComponentModel.DataAnnotations.Schema;

namespace Crud.Entity
{
    public class Student
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public Guid Semester_id { get; set; }

        // Navigation Properties,i.e Foreign Key Relationship
        [ForeignKey("Semester_id")]
        public Semester? Semester { get; set; }
    }
}
