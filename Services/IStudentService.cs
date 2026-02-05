using practicing.Dtos;
using Controller;
using StudentController.cs;

namespace practicing.Services
{
    public class IStudentService
    {
        Task<StudentDto>InsertStudent(StudentDto dto);
        Task<StudentDto> LinkSemester(int studentId, int semesterId);

    }
}
