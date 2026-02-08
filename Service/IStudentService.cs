namespace Crud.Service
using Microsoft.EntityFrameworkCore;
using Controllers;
using Dtos;
using Entity;
using Data;

{
    public class IStudentService
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
    Task<Student> GetStudent(int id);
    Task<List<Student>> GetAllStudents();

}
