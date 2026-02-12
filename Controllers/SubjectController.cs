using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using practicing.Data;
using practicing.Domain.Dtos;
using practicing.Entity;

namespace practicing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly AppDbContext _context;
        public SubjectController(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Add(SubjectDto dto)
        {
            var Sinfo = new Subject
            {
                Name = dto.Name,
                Description = dto.Description
            };
            _context.Subjects.Add(Sinfo);
            await _context.SaveChangesAsync();
            return Ok("Insert Successfully");
        }

        [HttpGet]
        [Route("{Id:int}")]
        public async Task<IActionResult> GetSubject(int Id)
        {
            var data = await _context.Subjects.FirstOrDefaultAsync();
            if(data is null)
            {
                return NotFound();
            }
            return Ok(data);
        }


        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var data = await _context.Subjects.FindAsync(Id);
            if (data is null)
                return NotFound();
            _context.Subjects.Remove(data);
            await _context.SaveChangesAsync();

            return Ok("Deleted Successfully");
        }
    }
}
