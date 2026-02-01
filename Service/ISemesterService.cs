using Crud.DTO;

namespace Crud.Service
{
    public interface ISemesterService
    {
        ServiceResult<IEnumerable<SemesterDTO>> GetAll();
        ServiceResult<SemesterDTO> GetById(Guid id);
        ServiceResult<SemesterDTO> Create(SemesterDTO semesterDto);
    }
}
