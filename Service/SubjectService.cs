using Crud.Data;
using Crud.DTO;
using Crud.Entity;

namespace Crud.Service
{
    public class SubjectService : ISubjectService
    {
        private readonly ApplicationDBContext _context;

        public SubjectService(ApplicationDBContext context)
        {
            _context = context;
        }

        public ServiceResult<IEnumerable<SubjectDTO>> GetAll()
        {
            try
            {
                var subjects = _context.Subjects
                    .Select(s => new SubjectDTO
                    {
                        Id = s.Id,
                        Name = s.Name,
                        description = s.description
                    })
                    .ToList();

                return ServiceResult<IEnumerable<SubjectDTO>>.SuccessResult(subjects);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<SubjectDTO>>.FailureResult($"Error retrieving subjects: {ex.Message}");
            }
        }

        public ServiceResult<SubjectDTO> GetById(Guid id)
        {
            try
            {
                var subject = _context.Subjects.Find(id);

                if (subject == null)
                    return ServiceResult<SubjectDTO>.FailureResult("Subject not found");

                var subjectDto = new SubjectDTO
                {
                    Id = subject.Id,
                    Name = subject.Name,
                    description = subject.description
                };

                return ServiceResult<SubjectDTO>.SuccessResult(subjectDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<SubjectDTO>.FailureResult($"Error retrieving subject: {ex.Message}");
            }
        }

        public ServiceResult<SubjectDTO> Create(SubjectDTO subjectDto)
        {
            try
            {
                var subject = new Subject
                {
                    Id = Guid.NewGuid(),
                    Name = subjectDto.Name,
                    description = subjectDto.description
                };

                _context.Subjects.Add(subject);
                _context.SaveChanges();

                subjectDto.Id = subject.Id;

                return ServiceResult<SubjectDTO>.SuccessResult(subjectDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<SubjectDTO>.FailureResult($"Error creating subject: {ex.Message}");
            }
        }
    }
}
