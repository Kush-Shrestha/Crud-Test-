using Crud.Data;
using Crud.DTO;
using Crud.Entity;

namespace Crud.Service
{
    public class SemesterService : ISemesterService
    {
        private readonly ApplicationDBContext _context;

        public SemesterService(ApplicationDBContext context)
        {
            _context = context;
        }

        public ServiceResult<IEnumerable<SemesterDTO>> GetAll()
        {
            try
            {
                var semesters = _context.Semester
                    .Select(s => new SemesterDTO
                    {
                        Id = s.Id,
                        Name = s.Name
                    })
                    .ToList();

                return ServiceResult<IEnumerable<SemesterDTO>>.SuccessResult(semesters);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<SemesterDTO>>.FailureResult($"Error retrieving semesters: {ex.Message}");
            }
        }

        public ServiceResult<SemesterDTO> GetById(Guid id)
        {
            try
            {
                var semester = _context.Semester.Find(id);

                if (semester == null)
                    return ServiceResult<SemesterDTO>.FailureResult("Semester not found");

                var semesterDto = new SemesterDTO
                {
                    Id = semester.Id,
                    Name = semester.Name
                };

                return ServiceResult<SemesterDTO>.SuccessResult(semesterDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<SemesterDTO>.FailureResult($"Error retrieving semester: {ex.Message}");
            }
        }

        public ServiceResult<SemesterDTO> Create(SemesterDTO semesterDto)
        {
            try
            {
                var semester = new Semester
                {
                    Id = Guid.NewGuid(),
                    Name = semesterDto.Name
                };

                _context.Semester.Add(semester);
                _context.SaveChanges();

                semesterDto.Id = semester.Id;

                return ServiceResult<SemesterDTO>.SuccessResult(semesterDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<SemesterDTO>.FailureResult($"Error creating semester: {ex.Message}");
            }
        }
    }
}
