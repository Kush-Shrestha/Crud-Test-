using System.ComponentModel.DataAnnotations;

namespace practicing.Domain.Dtos
{
    public class StudentDtoRead
    {
        [Required]
        public string Name { get; set; }
        public SemesterDto? Semester { get; set; }

    }
}
