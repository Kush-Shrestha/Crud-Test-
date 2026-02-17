using Crud.Application.Repository;
using practicing.Domain.Dtos;
using practicing.Domain.Entity;
using System.Security.Cryptography;
using System.Text;

namespace Crud.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<AuthResponseDto> Register(RegisterDto dto)
        {
            // Check if user already exists
            if (await _authRepository.UserExists(dto.Email))
            {
                return new AuthResponseDto
                {
                    Message = "User with this email already exists"
                };
            }

            // Hash password
            var passwordHash = HashPassword(dto.Password);

            // Create user
            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = passwordHash,
                CreatedAt = DateTime.UtcNow
            };

            var createdUser = await _authRepository.CreateUser(user);

            return new AuthResponseDto
            {
                UserId = createdUser.Id,
                Username = createdUser.Username,
                Email = createdUser.Email,
                Message = "Registration successful"
            };
        }

        public async Task<AuthResponseDto> Login(LoginDto dto)
        {
            // Find user by email
            var user = await _authRepository.GetUserByEmail(dto.Email);

            if (user == null)
            {
                return new AuthResponseDto
                {
                    Message = "Invalid email or password"
                };
            }

            // Verify password
            if (!VerifyPassword(dto.Password, user.PasswordHash))
            {
                return new AuthResponseDto
                {
                    Message = "Invalid email or password"
                };
            }

            return new AuthResponseDto
            {
                UserId = user.Id,
                Username = user.Username,
                Email = user.Email,
                Message = "Login successful"
            };
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        private bool VerifyPassword(string password, string hash)
        {
            var passwordHash = HashPassword(password);
            return passwordHash == hash;
        }
    }
}
