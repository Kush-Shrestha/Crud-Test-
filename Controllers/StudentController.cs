using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Crud.Entity;
using Crud.Data;
using Crud.DTO;

namespace Crud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDBContext dbContext;
        public StudentController(ApplicationDBContext dBContext)
        {
            dbContext = dBContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var students = dbContext.Students.ToList();
            var studentDTO = new List<StudentDTO>();
            foreach (var student in students)
            {
                studentDTO.Add(new StudentDTO
                {
                    Id = student.Id,
                    Name = student.Name,
                    Semester_id = student.Semester_id
                });
            }
            return Ok(studentDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var student = dbContext.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }
            var studentDTO = new StudentDTO
            {
                Id = student.Id,
                Name = student.Name,
                Semester_id = student.Semester_id
            };
            return Ok(studentDTO);
        }

        [HttpPost]
        public IActionResult Create([FromBody] StudentDTO studentDto)
        {
            //map or convert DTO to Entity
            var student = new Student
            {
                Id = Guid.NewGuid(),
                Name = studentDto.Name,
                Semester_id = studentDto.Semester_id
            };
            dbContext.Students.Add(student);
            dbContext.SaveChanges();

            var createdStudentDTO = new StudentDTO
            {
                Id = student.Id,
                Name = student.Name,
                Semester_id = student.Semester_id
            };
            return CreatedAtAction(nameof(GetById), new { id = student.Id }, createdStudentDTO);
        }
    }
}
