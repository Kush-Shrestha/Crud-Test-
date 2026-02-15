using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using practicing.Data;
using practicing.Domain.Dtos;
using practicing.Services;
using practicing.Domain.Entity;
using System.Net.WebSockets;

namespace practicing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        [HttpPost]
        public async Task<IActionResult> InsertStudent(StudentDto dto)
        {
           var SInfo = await _studentService.InsertStudent(dto);
            return Ok(SInfo);
        }

        [HttpPost("Link_Semester")]
        public async Task<IActionResult> LinkSemester(int studentId, int semesterId)
        {
            try
            {
                var result = await _studentService.LinkSemester(studentId, semesterId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Getall()
        {
            var result = await _studentService.Getall();
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

            var student = await _studentService.GetStudentById(Id);

            if (student == null)
                return NotFound();

            return Ok(student);
        }


        [HttpDelete]
        [Route("{Id:int}")]

        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                var result = await _studentService.Delete(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
