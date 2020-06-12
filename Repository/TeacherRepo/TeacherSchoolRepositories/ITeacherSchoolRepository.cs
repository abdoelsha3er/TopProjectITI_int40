using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopProjectITI_int40.Models;

namespace TopProjectITI_int40.Repository.TeacherRepo.TeacherSchoolRepositories
{
    public interface ITeacherSchoolRepository
    {
        Task<QueryResult<TeacherSchool>> GetTeacherSchools(int teacherId); // 
        Task<TeacherSchool> GetTeacherSchoolById(int teacherSchoolId); //
        Task AddTeacherSchool(TeacherSchool teacherSchool);
        Task DeleteTeacherSchool(TeacherSchool teacherSchool);
    }
}
