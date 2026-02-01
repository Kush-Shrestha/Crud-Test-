using Crud.Data;
using Crud.DTO;
using Crud.Entity;

namespace Crud.Service
{
    public class SemesterSubjectService : ISemesterSubjectService
    {
        private readonly ApplicationDBContext _context;

        public SemesterSubjectService(ApplicationDBContext context)
        {
            _context = context;
        }

        public ServiceResult<IEnumerable<SemesterSubjectDTO>> GetAll()
        {
            try
            {
                var semesterSubjects = _context.SemesterSubjects
                    .Select(ss => new SemesterSubjectDTO
                    {
                        Id = ss.Id,
                        Semester_id = ss.Semester_id,
                        Subject_id = ss.Subject_id
                    })
                    .ToList();

                return ServiceResult<IEnumerable<SemesterSubjectDTO>>.SuccessResult(semesterSubjects);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<SemesterSubjectDTO>>.FailureResult($"Error retrieving semester subjects: {ex.Message}");
            }
        }

        public ServiceResult<SemesterSubjectDTO> GetById(Guid id)
        {
            try
            {
                var semesterSubject = _context.SemesterSubjects.Find(id);

                if (semesterSubject == null)
                    return ServiceResult<SemesterSubjectDTO>.FailureResult("Semester subject not found");

                var semesterSubjectDto = new SemesterSubjectDTO
                {
                    Id = semesterSubject.Id,
                    Semester_id = semesterSubject.Semester_id,
                    Subject_id = semesterSubject.Subject_id
                };

                return ServiceResult<SemesterSubjectDTO>.SuccessResult(semesterSubjectDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<SemesterSubjectDTO>.FailureResult($"Error retrieving semester subject: {ex.Message}");
            }
        }

        public ServiceResult<SemesterSubjectDTO> Create(SemesterSubjectDTO semesterSubjectDto)
        {
            try
            {
                var semesterExists = _context.Semester.Any(s => s.Id == semesterSubjectDto.Semester_id);
                if (!semesterExists)
                    return ServiceResult<SemesterSubjectDTO>.FailureResult("Semester not found");

                var subjectExists = _context.Subjects.Any(s => s.Id == semesterSubjectDto.Subject_id);
                if (!subjectExists)
                    return ServiceResult<SemesterSubjectDTO>.FailureResult("Subject not found");

                var semesterSubject = new SemesterSubject
                {
                    Id = Guid.NewGuid(),
                    Semester_id = semesterSubjectDto.Semester_id,
                    Subject_id = semesterSubjectDto.Subject_id
                };

                _context.SemesterSubjects.Add(semesterSubject);
                _context.SaveChanges();

                semesterSubjectDto.Id = semesterSubject.Id;

                return ServiceResult<SemesterSubjectDTO>.SuccessResult(semesterSubjectDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<SemesterSubjectDTO>.FailureResult($"Error creating semester subject: {ex.Message}");
            }
        }
    }
}
