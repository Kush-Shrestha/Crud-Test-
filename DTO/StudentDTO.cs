namespace Crud.DTO
{
    public class StudentDTO
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required Guid Semester_id { get; set; }
    }
}
