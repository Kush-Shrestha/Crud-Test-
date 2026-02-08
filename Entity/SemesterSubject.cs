using System.ComponentModel.DataAnnotations.Schema;

namespace Crud.Entity
{
    public class Semester_Subject
    {
        public int Id { get; set; }
        
        [ForeignKey("Semester")]
        public int? SemesterId { get; set; }
        public Semester? Semester { get; set; }

        [ForeignKey("Subject")]
        public int? SubjectId { get; set; }
        public Subject? Subject { get; set; }
    }
}
