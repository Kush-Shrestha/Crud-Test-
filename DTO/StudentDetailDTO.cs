namespace Crud.DTO
{
    public class StudentDetailDTO
    {
        public Guid StudentId { get; set; }
        public string? StudentName { get; set; } = null;
        public Guid SemesterId { get; set; }
        public string? SemesterName { get; set; }
        public ICollection<SubjectInfoDTO> Subjects { get; set; }
    }

    public class SubjectInfoDTO
    {
        public Guid SubjectId { get; set; }
        public string? SubjectName { get; set; }
        public string? Description { get; set; }
    }
}
