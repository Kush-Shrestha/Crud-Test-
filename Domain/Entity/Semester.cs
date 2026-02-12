using System.ComponentModel.DataAnnotations;

namespace practicing.Domain.Entity
{
    public class Semester
    {
        public int Id { get; set; }
        [Required]
        public  string Name { get; set; }

        //public ICollection<Student>? student { get; set; }
        public ICollection<AssignSubject>? join { get; set; }

    }
}
