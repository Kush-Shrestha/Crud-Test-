namespace Crud.Entity
{
    public class SemesterSubject
    {
        public Guid Id { get; set;}
        public Guid Semester_id { get; set; }
        public Guid Subject_id { get; set; }
        public Semester? Semester { get; set; }
        public Subject? Subject { get; set; }
    }
}
