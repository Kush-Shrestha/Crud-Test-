using practicing.Domain.Dtos;
using practicing.Domain.Entity;

namespace Crud.Application.Repository
{
    public interface IAuthRepository
    {
        Task<User?> GetUserByEmail(string email);
        Task<User?> GetUserByUsername(string username);
        Task<User> CreateUser(User user);
        Task<bool> UserExists(string email);
    }
}
