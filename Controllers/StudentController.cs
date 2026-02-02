using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using practicing.Data;
using practicing.Dtos;
using practicing.Entity;
using System.Net.WebSockets;

namespace practicing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentController(AppDbContext dbContext)
        {
            _context = dbContext;
        }
        [HttpPost]
        public async Task<IActionResult> InsertStudent(StudentDto dto)
        {
            var SInfo = new Student
            {
                Name = dto.Name,
                semesterId = dto.semesterId
            };
            _context.Students.Add(SInfo);
            await _context.SaveChangesAsync();
            return Ok("Added Successfully");
        }

        [HttpPost("Link_Semester")]
        public async Task<IActionResult> LinkSemester(int studentId, int semesterId)
        {
            var student = await _context.Students.FindAsync(studentId);
            var semester = await _context.Semesters.FindAsync(semesterId);

            if(studentId == null ||  semesterId == null){
                return NotFound("Student or Semester not Found");
            }
            student.semester = semester;
            await _context.SaveChangesAsync();

            return Ok("Student assigned to Semester successfully");
        }

        [HttpGet]
        public async Task<IActionResult> Getall()
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
            //var data = await _context.Students
            //    .Select(s => new StudentDto
            //    {
            //        Name = s.Name,
            //        semesterId = s.semesterId,
            //    })
            //    .ToListAsync();

            if (!result.Any())
                return NotFound();


            return Ok(result);
        }


        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetStudentById(int Id)
        {
            //var student = await _context.Students
            //    .Where(s => s.Id == Id)
            //    .Select(s => new StudentDtoRead
            //    {
            //        Name = s.Name,
            //        Semester = new SemesterDto
            //        {
            //            Name = s.semester.Name,

            //        }


            //    })
            //    .FirstOrDefaultAsync();

            var student =await  _context.Students
                .Include(x => x.semester)
                .ThenInclude(x=> x.join)
                .ThenInclude( x => x.subject)
                 .FirstOrDefaultAsync();



            if (student == null)
                return NotFound();

            return Ok(student);
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
