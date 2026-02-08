using practicing.Dtos;

namespace practicing.Services
{
    public class IStudentService
    {
        Task<StudentDto>InsertStudent(StudentDto dto);
        Task<StudentDto> LinkSemester(int studentId, int semesterId);
        Task<StudentDto> Getall();
        Task<Student> GetStudentById();

    }
}
