using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using practicing.Data;
using practicing.Domain.Entity;
using practicing.Dtos;

namespace practicing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SemesterSubjectController : ControllerBase
    {
        private readonly AppDbContext _context;
        
        public SemesterSubjectController(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Add(SemesterSubjectDto dto)
        {
            var semesterSubject = new Semester_Subject
            {
                SemesterId = dto.SemesterId,
                SubjectId = dto.SubjectId
            };
            _context.Set<Semester_Subject>().Add(semesterSubject);
            await _context.SaveChangesAsync();
            return Ok("Semester-Subject link created successfully");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _context.Set<Semester_Subject>()
                .Include(ss => ss.Semester)
                .Include(ss => ss.Subject)
                .Select(ss => new
                {
                    ss.Id,
                    Semester = ss.Semester == null ? null : new
                    {
                        ss.Semester.Id,
                        ss.Semester.Name
                    },
                    Subject = ss.Subject == null ? null : new
                    {
                        ss.Subject.Id,
                        ss.Subject.Name,
                        ss.Subject.Description
                    }
                })
                .ToListAsync();
            
            if (!data.Any())
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpGet]
        [Route("{Id:int}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var data = await _context.Set<Semester_Subject>()
                .Include(ss => ss.Semester)
                .Include(ss => ss.Subject)
                .Where(ss => ss.Id == Id)
                .Select(ss => new
                {
                    ss.Id,
                    Semester = ss.Semester == null ? null : new
                    {
                        ss.Semester.Id,
                        ss.Semester.Name
                    },
                    Subject = ss.Subject == null ? null : new
                    {
                        ss.Subject.Id,
                        ss.Subject.Name,
                        ss.Subject.Description
                    }
                })
                .FirstOrDefaultAsync();
            
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
            var data = await _context.Set<Semester_Subject>().FindAsync(Id);
            if (data is null)
                return NotFound();
            
            _context.Set<Semester_Subject>().Remove(data);
            await _context.SaveChangesAsync();

            return Ok("Semester-Subject link deleted successfully");
        }
    }
}
