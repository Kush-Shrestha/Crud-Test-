using practicing.Dtos;
using practicing.Entity;

namespace practicing.Services
{
    public interface IStudentService
    {
        Task<string> InsertStudent(StudentDto dto);
        Task<string> LinkSemester(int studentId, int semesterId);
        Task<List<StudentDtoRead>> Getall();
        Task<Student> GetStudentById(int Id);
        Task<string> Delete(int Id);
    }
}
