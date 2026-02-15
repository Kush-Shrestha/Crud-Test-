using practicing.Domain.Dtos;
using Crud.Application.Repository;

namespace Crud.Application.Services
{
    public class AssignSubjectService : IAssignSubjectService
    {
        private readonly IAssignSubjectRepository _repository;

        public AssignSubjectService(IAssignSubjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> Add(AssignSubjectDto dto)
        {
            return await _repository.Add(dto);
        }
    }
}
