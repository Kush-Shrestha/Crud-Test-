using Crud.DTO;

namespace Crud.Service
{
    public interface ISubjectService
    {
        ServiceResult<IEnumerable<SubjectDTO>> GetAll();
        ServiceResult<SubjectDTO> GetById(Guid id);
        ServiceResult<SubjectDTO> Create(SubjectDTO subjectDto);
    }
}
