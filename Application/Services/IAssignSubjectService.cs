using practicing.Domain.Dtos;

namespace Crud.Application.Services
{
    public interface IAssignSubjectService
    {
        Task<string> Add(AssignSubjectDto dto);
    }
}
