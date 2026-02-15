using Microsoft.EntityFrameworkCore;
using practicing.Data;
using practicing.Domain.Dtos;
using practicing.Domain.Entity;

namespace Crud.Application.Repository
{
    public class AssignSubjectRepository : IAssignSubjectRepository
    {
        private readonly AppDbContext _context;

        public AssignSubjectRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> Add(AssignSubjectDto dto)
        {
            var semesterExists = await _context.Semesters.AnyAsync(s => s.Id == dto.semesterId);
            var subjectExists = await _context.Subjects.AnyAsync(s => s.Id == dto.subjectId);

            if (!semesterExists || !subjectExists)
            {
                return "Invalid Semester or Subject ID.";
            }

            var link = new AssignSubject
            {
                semesterId = dto.semesterId,
                subjectId = dto.subjectId
            };

            _context.Semester_Subjects.Add(link);
            await _context.SaveChangesAsync();
            return "Link Successfully";
        }

        public async Task<List<AssignSubjectDto>> GetAll()
        {
            var result = await _context.Semester_Subjects
                .Select(a => new AssignSubjectDto
                {
                    semesterId = a.semesterId,
                    subjectId = a.subjectId
                })
                .ToListAsync();

            return result;
        }

        public async Task<AssignSubjectDto?> GetById(int id)
        {
            var assignSubject = await _context.Semester_Subjects
                .Where(a => a.Id == id)
                .Select(a => new AssignSubjectDto
                {
                    semesterId = a.semesterId,
                    subjectId = a.subjectId
                })
                .FirstOrDefaultAsync();

            return assignSubject;
        }

        public async Task<string> Delete(int id)
        {
            var assignSubject = await _context.Semester_Subjects.FindAsync(id);
            
            if (assignSubject == null)
            {
                return "Assignment not found";
            }

            _context.Semester_Subjects.Remove(assignSubject);
            await _context.SaveChangesAsync();
            
            return "Deleted Successfully";
        }
    }
}
