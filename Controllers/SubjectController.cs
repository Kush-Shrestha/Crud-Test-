using Microsoft.AspNetCore.Mvc;
using Crud.DTO;
using Crud.Service;

namespace Crud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _subjectService.GetAll();

            if (!result.Success)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(result.Data);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var result = _subjectService.GetById(id);

            if (!result.Success)
                return NotFound(new { message = result.ErrorMessage });

            return Ok(result.Data);
        }

        [HttpPost]
        public IActionResult Create([FromBody] SubjectDTO subjectDto)
        {
            var result = _subjectService.Create(subjectDto);

            if (!result.Success)
                return BadRequest(new { message = result.ErrorMessage });

            return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result.Data);
        }
    }
}
