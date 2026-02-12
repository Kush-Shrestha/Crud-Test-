using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace practicing.Domain.Dtos
{
    public class StudentDto
    {
        [Required]
        public string Name { get; set; }
        public int? semesterId { get; set; }
    }
}
