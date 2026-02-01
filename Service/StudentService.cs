using Crud.Data;
using Crud.DTO;
using Crud.Entity;
using Microsoft.EntityFrameworkCore;

namespace Crud.Service
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDBContext _context;

        public StudentService(ApplicationDBContext context)
        {
            _context = context;
        }

        public ServiceResult<IEnumerable<StudentDTO>> GetAll()
        {
            try
            {
                var students = _context.Students
                    .Select(s => new StudentDTO
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Semester_id = s.Semester_id
                    })
                    .ToList();

                return ServiceResult<IEnumerable<StudentDTO>>.SuccessResult(students);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<StudentDTO>>.FailureResult($"Error retrieving students: {ex.Message}");
            }
        }

        public ServiceResult<StudentDTO> GetById(Guid id)
        {
            try
            {
                var student = _context.Students.Find(id);

                if (student == null)
                    return ServiceResult<StudentDTO>.FailureResult("Student not found");

                var studentDto = new StudentDTO
                {
                    Id = student.Id,
                    Name = student.Name,
                    Semester_id = student.Semester_id
                };

                return ServiceResult<StudentDTO>.SuccessResult(studentDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<StudentDTO>.FailureResult($"Error retrieving student: {ex.Message}");
            }
        }

        public ServiceResult<StudentDetailDTO> GetStudentDetails(Guid id)
        {
            try
            {
                var student = _context.Students
                    .Include(s => s.Semester)
                    .FirstOrDefault(s => s.Id == id);

                if (student == null)
                    return ServiceResult<StudentDetailDTO>.FailureResult("Student not found");

                var subjects = _context.SemesterSubjects
                    .Where(ss => ss.Semester_id == student.Semester_id)
                    .Include(ss => ss.Subject)
                    .Select(ss => new SubjectInfoDTO
                    {
                        SubjectId = ss.Subject!.Id,
                        SubjectName = ss.Subject.Name,
                        Description = ss.Subject.description
                    })
                    .ToList();

                var studentDetail = new StudentDetailDTO
                {
                    StudentId = student.Id,
                    StudentName = student.Name,
                    SemesterId = student.Semester_id,
                    SemesterName = student.Semester?.Name ?? "Unknown",
                    Subjects = subjects
                };

                return ServiceResult<StudentDetailDTO>.SuccessResult(studentDetail);
            }
            catch (Exception ex)
            {
                return ServiceResult<StudentDetailDTO>.FailureResult($"Error retrieving student details: {ex.Message}");
            }
        }

        public ServiceResult<StudentDTO> Create(StudentDTO studentDto)
        {
            try
            {
                var semesterExists = _context.Semester.Any(s => s.Id == studentDto.Semester_id);
                if (!semesterExists)
                    return ServiceResult<StudentDTO>.FailureResult("Semester not found");

                var student = new Student
                {
                    Id = Guid.NewGuid(),
                    Name = studentDto.Name,
                    Semester_id = studentDto.Semester_id
                };

                _context.Students.Add(student);
                _context.SaveChanges();

                studentDto.Id = student.Id;

                return ServiceResult<StudentDTO>.SuccessResult(studentDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<StudentDTO>.FailureResult($"Error creating student: {ex.Message}");
            }
        }
    }
}
