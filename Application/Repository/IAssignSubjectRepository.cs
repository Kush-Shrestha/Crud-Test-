using practicing.Domain.Dtos;

namespace Crud.Application.Repository
{
    public interface IAssignSubjectRepository
    {
        Task<string> Add(AssignSubjectDto dto);
        Task<List<AssignSubjectDto>> GetAll();
        Task<AssignSubjectDto?> GetById(int id);
        Task<string> Delete(int id);
    }
}
