using System.ComponentModel.DataAnnotations;

namespace Crud.dtos
{
    public class StudentDetailDTO
    {
        [Required]
        public string Name { get; set; }
        public SemesterDTO? Semester { get; set; }

    }
}
