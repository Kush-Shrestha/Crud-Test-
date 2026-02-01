using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Crud.Data;
using Crud.dtos;
using Crud.Entity;

namespace Crud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        
        public SubjectController(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Add(SubjectDTO dto)
        {
            var subjectInfo = new Subject
            {
                Name = dto.Name,
                Description = dto.Description
            };
            _context.Subjects.Add(subjectInfo);
            await _context.SaveChangesAsync();
            return Ok("Inserted Successfully");
        }

        [HttpPost("Link_Semester")]
        public async Task<IActionResult> LinkSemester(int subjectId, int semesterId)
        {
            var subject = await _context.Subjects.FindAsync(subjectId);
            var semester = await _context.Semesters.FindAsync(semesterId);

            if (subject == null || semester == null)
            {
                return NotFound("Subject or Semester not Found");
            }

            // Check if the link already exists
            var existingLink = await _context.Semester_Subjects
                .FirstOrDefaultAsync(ss => ss.SemesterId == semesterId && ss.SubjectId == subjectId);

            if (existingLink != null)
            {
                return BadRequest("This Subject-Semester link already exists");
            }

            var semesterSubject = new Semester_Subject
            {
                SemesterId = semesterId,
                SubjectId = subjectId
            };

            _context.Semester_Subjects.Add(semesterSubject);
            await _context.SaveChangesAsync();

            return Ok("Semester linked to Subject successfully");
        }

        [HttpDelete("Unlink_Semester")]
        public async Task<IActionResult> UnlinkSemester(int subjectId, int semesterId)
        {
            var link = await _context.Semester_Subjects
                .FirstOrDefaultAsync(ss => ss.SemesterId == semesterId && ss.SubjectId == subjectId);

            if (link == null)
            {
                return NotFound("Subject-Semester link not found");
            }

            _context.Semester_Subjects.Remove(link);
            await _context.SaveChangesAsync();

            return Ok("Semester unlinked from Subject successfully");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _context.Subjects.ToListAsync();
            if (!data.Any())
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpGet]
        [Route("{Id:int}")]
        public async Task<IActionResult> GetSubject(int Id)
        {
            var data = await _context.Subjects
                .Include(s => s.SemesterSubjects)
                .ThenInclude(ss => ss.Semester)
                .Where(s => s.Id == Id)
                .Select(s => new
                {
                    s.Id,
                    s.Name,
                    s.Description,
                    Semesters = s.SemesterSubjects != null
                        ? s.SemesterSubjects.Where(ss => ss.Semester != null).Select(ss => new
                        {
                            ss.Semester!.Id,
                            ss.Semester.Name
                        })
                        : Enumerable.Empty<object>()
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
            var data = await _context.Subjects.FindAsync(Id);
            if (data is null)
                return NotFound();
            
            _context.Subjects.Remove(data);
            await _context.SaveChangesAsync();

            return Ok("Deleted Successfully");
        }
    }
}
