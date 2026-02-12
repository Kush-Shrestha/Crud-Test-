using Microsoft.EntityFrameworkCore;
using practicing.Data;
using practicing.Domain.Dtos;
using practicing.Domain.Entity;

namespace practicing.Application.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;

        public StudentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> InsertStudent(StudentDto dto)
        {
            var SInfo = new Student
            {
                Name = dto.Name,
                semesterId = dto.semesterId
            };
            _context.Students.Add(SInfo);
            await _context.SaveChangesAsync();
            return "Added Successfully";
        }

        public async Task<string> LinkSemester(int studentId, int semesterId)
        {
            var student = await _context.Students.FindAsync(studentId);
            var semester = await _context.Semesters.FindAsync(semesterId);

            if (student == null || semester == null)
            {
                throw new Exception("Student or Semester not Found");
            }
            student.semester = semester;
            await _context.SaveChangesAsync();

            return "Student assigned to Semester successfully";
        }

        public async Task<List<StudentDtoRead>> Getall()
        {
            var result = await _context.Students
                .Include(s => s.semester)
                .Select(s => new StudentDtoRead
                {
                    Name = s.Name,
                    Semester = s.semester == null ? null : new SemesterDto
                    {
                        Name = s.semester.Name
                    }
                })
                .ToListAsync();

            return result;
        }

        public async Task<Student> GetStudentById(int Id)
        {
            var student = await _context.Students
                .Include(x => x.semester)
                .ThenInclude(x => x.join)
                .ThenInclude(x => x.subject)
                .FirstOrDefaultAsync(x => x.Id == Id);

            return student;
        }

        public async Task<string> Delete(int Id)
        {
            var student = await _context.Students.FindAsync(Id);
            if (student is null)
            {
                throw new Exception("Student not found");
            }
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return "Deleted Successfully";
        }
    }
}
