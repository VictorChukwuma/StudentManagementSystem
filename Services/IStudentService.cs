using StudentManagementSystem.DTOs;
using StudentManagementSystem.Models;
using System.Threading.Tasks;

namespace StudentManagementSystem.Services
{
    // Interface defines all methods the service must implement
        public interface IStudentService
            {
                    // Register a new student
                            Task<Student> RegisterStudentAsync(RegisterStudentDto dto);

                                    // Fetch a student by email (for login)
                                            Task<Student?> GetStudentByEmailAsync(string email);

                                            Task<Student?> GetStudentByIdAsync(int id);
                                            Task DeleteStudentAsync(Student student);

                                            Task<IEnumerable<Student>> GetAllStudentsAsync();
                                                }
                                                }