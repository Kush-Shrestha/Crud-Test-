using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Crud.dtos
{
    public class StudentDTO
    {
        [Required]
        public string Name { get; set; }
        public int? SemesterId { get; set; }
    }
}
