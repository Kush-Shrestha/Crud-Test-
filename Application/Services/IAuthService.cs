using practicing.Domain.Dtos;

namespace Crud.Application.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> Register(RegisterDto dto);
        Task<AuthResponseDto> Login(LoginDto dto);
    }
}
