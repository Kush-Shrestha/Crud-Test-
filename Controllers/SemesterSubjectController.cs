using Microsoft.AspNetCore.Mvc;
using Crud.DTO;
using Crud.Service;

namespace Crud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SemesterSubjectController : ControllerBase
    {
        private readonly ISemesterSubjectService _semesterSubjectService;

        public SemesterSubjectController(ISemesterSubjectService semesterSubjectService)
        {
            _semesterSubjectService = semesterSubjectService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _semesterSubjectService.GetAll();

            if (!result.Success)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(result.Data);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var result = _semesterSubjectService.GetById(id);

            if (!result.Success)
                return NotFound(new { message = result.ErrorMessage });

            return Ok(result.Data);
        }

        [HttpPost]
        public IActionResult Create([FromBody] SemesterSubjectDTO semesterSubjectDto)
        {
            var result = _semesterSubjectService.Create(semesterSubjectDto);

            if (!result.Success)
                return BadRequest(new { message = result.ErrorMessage });

            return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result.Data);
        }
    }
}
