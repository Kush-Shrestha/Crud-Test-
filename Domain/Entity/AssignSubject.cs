using System.ComponentModel.DataAnnotations.Schema;

namespace practicing.Domain.Entity
{
    public class AssignSubject
    {
        public int Id { get; set; }
        [ForeignKey("semester")]
        public int? semesterId { get; set; }
        public Semester? semester { get; set; }

        [ForeignKey("subject")]
        public int? subjectId { get; set; }
        public Subject? subject { get; set; }
    }
}
