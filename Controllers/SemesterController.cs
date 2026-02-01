using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Crud.Data;
using Crud.dtos;
using Crud.Entity;

namespace Crud.Controllers
{
    [Route("api/[controller]", Name = "Semester")]
    [ApiController]
    public class SemesterController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        
        public SemesterController(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

  
        [HttpPost]
        public async Task<IActionResult> Add(SemesterDTO dto)
        {
            var semesterInfo = new Semester
            {
                Name = dto.Name
            };
            _context.Semesters.Add(semesterInfo);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { Id = semesterInfo.Id }, semesterInfo);
        }

        
        [HttpPost("Link_Subject")]
        public async Task<IActionResult> LinkSubject(int semesterId, int subjectId)
        {
            var semester = await _context.Semesters.FindAsync(semesterId);
            var subject = await _context.Subjects.FindAsync(subjectId);

            if (semester == null || subject == null)
            {
                return NotFound("Semester or Subject not Found");
            }

            // Check if the link already exists
            var existingLink = await _context.Semester_Subjects
                .FirstOrDefaultAsync(ss => ss.SemesterId == semesterId && ss.SubjectId == subjectId);

            if (existingLink != null)
            {
                return BadRequest("This Semester-Subject link already exists");
            }

            var semesterSubject = new Semester_Subject
            {
                SemesterId = semesterId,
                SubjectId = subjectId
            };

            _context.Semester_Subjects.Add(semesterSubject);
            await _context.SaveChangesAsync();

            return Ok("Subject linked to Semester successfully");
        }

        
        [HttpDelete("Unlink_Subject")]
        public async Task<IActionResult> UnlinkSubject(int semesterId, int subjectId)
        {
            var link = await _context.Semester_Subjects
                .FirstOrDefaultAsync(ss => ss.SemesterId == semesterId && ss.SubjectId == subjectId);

            if (link == null)
            {
                return NotFound("Semester-Subject link not found");
            }

            _context.Semester_Subjects.Remove(link);
            await _context.SaveChangesAsync();

            return Ok("Subject unlinked from Semester successfully");
        }

        /// <summary>
        /// Gets all semesters.
        /// </summary>
        /// <returns>An IActionResult containing the list of semesters.</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _context.Semesters.ToListAsync();
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
            var data = await _context.Semesters
                .Include(s => s.SemesterSubjects)
                .ThenInclude(sj => sj.Subject)
                .Where(s => s.Id == Id)
                .Select(s => new
                {
                    s.Id,
                    s.Name,
                    Subjects = s.SemesterSubjects != null 
                        ? s.SemesterSubjects.Where(sj => sj.Subject != null).Select(sj => new
                        {
                            sj.Subject!.Id,
                            sj.Subject.Name,
                            sj.Subject.Description
                        }) 
                        : Enumerable.Empty<object>()
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
