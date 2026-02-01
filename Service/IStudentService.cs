using Crud.DTO;

namespace Crud.Service
{
    public interface IStudentService
    {
        ServiceResult<IEnumerable<StudentDTO>> GetAll();
        ServiceResult<StudentDTO> GetById(Guid id);
        ServiceResult<StudentDetailDTO> GetStudentDetails(Guid id);
        ServiceResult<StudentDTO> Create(StudentDTO studentDto);
    }
}
