using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Crud.Data;
using Crud.dtos;
using Crud.Entity;
using System.Net.WebSockets;

namespace Crud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        
        [HttpPost]
        public async Task<IActionResult> InsertStudent(StudentDTO dto)
        {
            var studentInfo = new Student
            {
                Name = dto.Name,
                SemesterId = dto.SemesterId
            };
            _context.Students.Add(studentInfo);
            await _context.SaveChangesAsync();
            return Ok("Added Successfully");
        }

        [HttpPost("Link_Semester")]
        public async Task<IActionResult> LinkSemester(int studentId, int semesterId)
        {
            var student = await _context.Students.FindAsync(studentId);
            var semester = await _context.Semesters.FindAsync(semesterId);

            if (student == null || semester == null)
            {
                return NotFound("Student or Semester not Found");
            }
            
            student.Semester = semester;
            await _context.SaveChangesAsync();

            return Ok("Student assigned to Semester successfully");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _context.Students
                .Include(s => s.Semester)
                .Select(s => new StudentDetailDTO
                {
                    Name = s.Name,
                    Semester = s.Semester == null ? null : new SemesterDTO
                    {
                        Name = s.Semester.Name
                    }
                })
                .ToListAsync();

            if (!result.Any())
                return NotFound();

            return Ok(result);
        }

        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetStudentById(int Id)
        {
            var student = await _context.Students
                .Include(s => s.Semester)
                    .ThenInclude(sem => sem.SemesterSubjects)
                    .ThenInclude(ss => ss.Subject)
                .Where(s => s.Id == Id)
                .FirstOrDefaultAsync();

            if (student == null)
                return NotFound("Student not found");

            var result = new
            {
                StudentId = student.Id,
                StudentName = student.Name,
                Semester = student.Semester == null ? null : new
                {
                    SemesterId = student.Semester.Id,
                    SemesterName = student.Semester.Name,
                    Subjects = student.Semester.SemesterSubjects
                        ?.Where(ss => ss.Subject != null)
                        .Select(ss => new
                        {
                            SubjectId = ss.Subject!.Id,
                            SubjectName = ss.Subject.Name,
                            Description = ss.Subject.Description
                        })
                        .ToList() ?? Enumerable.Empty<object>()
                }
            };

            return Ok(result);
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var student = await _context.Students.FindAsync(Id);
            if (student is null)
                return NotFound();
            
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return Ok("Deleted Successfully");
        }
    }
}
