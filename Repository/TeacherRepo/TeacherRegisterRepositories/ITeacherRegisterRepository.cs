using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopProjectITI_int40.Models;
using TopProjectITI_int40.ViewModels;

namespace TopProjectITI_int40.Repository.TeacherRepo.TeacherRegisterRepositories
{
    public interface ITeacherRegisterRepository
    {
        Task<IEnumerable<Teacher>> GetTeachers();
        Task TeacherRegister(Teacher teacher);
        Task<Teacher> GetTeacherById(int teacherId);
        Task<Teacher> LoginTeacher(TeacherViewModel teacherViewModel);
        Task EditTeacherProfile(Teacher newTeacher, int id, string file);
    }
}
