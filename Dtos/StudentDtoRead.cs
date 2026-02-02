using System.ComponentModel.DataAnnotations;

namespace practicing.Dtos
{
    public class StudentDtoRead
    {
        [Required]
        public string Name { get; set; }
        public SemesterDto? Semester { get; set; }

    }
}
