using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopProjectITI_int40.Models;

namespace TopProjectITI_int40.Repository.TeacherRepo.TeacherRegisterRepositories
{
    public interface ITeacherRegisterRepository
    {
        Task<IEnumerable<Teacher>> GetTeachers();
        Task TeacherRegister(Teacher teacher);
        Task<Teacher> GetTeacherById(int teacherId);
        Task EditTeacherProfile(Teacher newTeacher, int id);
    }
}
