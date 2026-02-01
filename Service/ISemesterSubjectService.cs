using Crud.DTO;

namespace Crud.Service
{
    public interface ISemesterSubjectService
    {
        ServiceResult<IEnumerable<SemesterSubjectDTO>> GetAll();
        ServiceResult<SemesterSubjectDTO> GetById(Guid id);
        ServiceResult<SemesterSubjectDTO> Create(SemesterSubjectDTO semesterSubjectDto);
    }
}
