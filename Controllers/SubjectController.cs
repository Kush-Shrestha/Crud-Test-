using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Crud.Entity;
using Crud.Data;
using Crud.DTO;

namespace Crud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ApplicationDBContext dbContext;
        public SubjectController(ApplicationDBContext dBContext)
        {
            dbContext = dBContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var subjects = dbContext.Subjects.ToList();
            var subjectDTO = new List<SubjectDTO>();
            foreach (var subject in subjects)
            {
                subjectDTO.Add(new SubjectDTO
                {
                    Id = subject.Id,
                    Name = subject.Name,
                    description = subject.description
                });
            }
            return Ok(subjectDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var subject = dbContext.Subjects.Find(id);
            if (subject == null)
            {
                return NotFound();
            }
            var subjectDTO = new SubjectDTO
            {
                Id = subject.Id,
                Name = subject.Name,
                description = subject.description
            };
            return Ok(subjectDTO);
        }

        [HttpPost]
        public IActionResult Create([FromBody] SubjectDTO subjectDto)
        {
            //map or convert DTO to Entity
            var subject = new Subject
            {
                Id = Guid.NewGuid(),
                Name = subjectDto.Name,
                description = subjectDto.description
            };
            dbContext.Subjects.Add(subject);
            dbContext.SaveChanges();

            var createdSubjectDTO = new SubjectDTO
            {
                Id = subject.Id,
                Name = subject.Name,
                description = subject.description
            };
            return CreatedAtAction(nameof(GetById), new { id = subject.Id }, createdSubjectDTO);
        }
    }
}
