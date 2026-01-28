namespace Crud.Entity
{
    public class Student
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public Guid Semester_id { get; set; }

        // Navigation Properties,i.e Foreign Key Relationship
        public Semester Semester { get; set; }
    }
}
