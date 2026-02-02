using System.ComponentModel.DataAnnotations.Schema;

namespace practicing.Entity
{
    public class Student
    {
        public int Id { get; set; }
        public required string Name{ get;set;}
        [ForeignKey("semester")]
        public int? semesterId { get; set; } // Need parent foreign key first

        public Semester? semester { get; set; }
    }
}
