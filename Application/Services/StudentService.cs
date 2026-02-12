using Microsoft.EntityFrameworkCore;
using practicing.Data;
using practicing.Dtos;
using practicing.Entity;
using practicing.Repository;

namespace practicing.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<string> InsertStudent(StudentDto dto)
        {
            return await _studentRepository.InsertStudent(dto);
        }

        public async Task<string> LinkSemester(int studentId, int semesterId)
        {
            return await _studentRepository.LinkSemester(studentId, semesterId);
        }

        public async Task<List<StudentDtoRead>> Getall()
        {
            return await _studentRepository.Getall();
        }

        public async Task<Student> GetStudentById(int Id)
        {
            return await _studentRepository.GetStudentById(Id);
        }

        public async Task<string> Delete(int Id)
        {
            return await _studentRepository.Delete(Id);
        }
    }
}
