using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using practicing.Data;
using practicing.Dtos;
using practicing.Entity;

namespace practicing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SemesterController : ControllerBase
    {
        private readonly AppDbContext _context;
        public SemesterController(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Add(SemesterDto dto)
        {
            var SInfo = new Semester
            {
                Name = dto.Name
            };
            _context.Semesters.Add(SInfo);
            await _context.SaveChangesAsync();
            return Ok("Insert Successfully");
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _context.Semesters.ToListAsync();
            if (data is null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpGet]
        [Route("{Id:int}")]
        public async Task<IActionResult> GetbyId(int Id)
        {
            var data = await _context.Semesters
                .Where(s => s.Id == Id)
                .Select(s => new
                {
                    s.Name,
                    Subjects = s.join.Select(sj => new
                    {
                        sj.subject.Id,
                        sj.subject.Name,
                        sj.subject.Description
                    })
                }).FirstOrDefaultAsync();
            if (data is null)
            {
                return NotFound();
            }

            return Ok(data);
        }


        [HttpDelete]
        [Route("{Id:int}")]

        public async Task<IActionResult> Delete(int Id)
        {
            var data = await _context.Semesters.FindAsync(Id);
            if (data is null)
                return NotFound();
            _context.Semesters.Remove(data);
            await _context.SaveChangesAsync();

            return Ok("Deleted Successfully");
        }
    }
}
