using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Crud.Application.Services;
using practicing.Domain.Dtos;

namespace practicing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignSubjectController : ControllerBase
    {
        private readonly IAssignSubjectService _assignSubjectService;
        
        public AssignSubjectController(IAssignSubjectService assignSubjectService)
        {
            _assignSubjectService = assignSubjectService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(AssignSubjectDto dto)
        {
            var result = await _assignSubjectService.Add(dto);
            
            if (result.Contains("Invalid"))
            {
                return BadRequest(result);
            }
            
            return Ok(result);
        }
    }
}
