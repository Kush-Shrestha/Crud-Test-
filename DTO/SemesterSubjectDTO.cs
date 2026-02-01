using Crud.Entity;

namespace Crud.DTO
{
    public class SemesterSubjectDTO
    {
        public Guid Id { get; set; }
        public Guid Semester_id { get; set; }
        public Guid Subject_id { get; set; }
    }
}
