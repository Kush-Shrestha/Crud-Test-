using Microsoft.AspNetCore.Mvc;
using Crud.DTO;
using Crud.Service;

namespace Crud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SemesterController : ControllerBase
    {
        private readonly ISemesterService _semesterService;

        public SemesterController(ISemesterService semesterService)
        {
            _semesterService = semesterService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _semesterService.GetAll();
            
            if (!result.Success)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(result.Data);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var result = _semesterService.GetById(id);

            if (!result.Success)
                return NotFound(new { message = result.ErrorMessage });

            return Ok(result.Data);
        }

        [HttpPost]
        public IActionResult Create([FromBody] SemesterDTO semesterDto)
        {
            var result = _semesterService.Create(semesterDto);

            if (!result.Success)
                return BadRequest(new { message = result.ErrorMessage });

            return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result.Data);
        }
    }
}
