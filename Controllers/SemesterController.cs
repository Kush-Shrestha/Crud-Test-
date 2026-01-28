using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Crud.Entity;
using Crud.Data;
using Crud.DTO;

namespace Crud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SemesterController : ControllerBase
    {
        private readonly ApplicationDBContext dbContext;
        public SemesterController(ApplicationDBContext dBContext)
        {
            dbContext = dBContext;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
          var semesters = dbContext.Semester.ToList();
            var semesterDTO = new List<SemesterDTO>();
            foreach(var sem in semesters)
            {
                semesterDTO.Add(new SemesterDTO
                {
                    Id=sem.Id,
                    Name=sem.Name
                });
            }
            return Ok(semesters);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var semester = dbContext.Semester.Find(id);
            if (semester == null)
            {
                return NotFound();
            }
            var semesterDTO = new SemesterDTO
            {
                Id = semester.Id,
                Name = semester.Name
            };
            return Ok(semesterDTO);
        }

        [HttpPost]
        public IActionResult Create([FromBody] SemesterDTO semesterDto)
        {
            //map or convert DTO to Entity
            var semester = new Semester
            {
                Id = Guid.NewGuid(),
                Name = semesterDto.Name
            };
            dbContext.Semester.Add(semester);
            dbContext.SaveChanges();

            var createdSemesterDTO = new SemesterDTO
            {
                Id = semester.Id,
                Name = semester.Name
            };
            return CreatedAtAction(nameof(GetById), new { id = semesterDto.Id }, semesterDto);

        }

    }
}
