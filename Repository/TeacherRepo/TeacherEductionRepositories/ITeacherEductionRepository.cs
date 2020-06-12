using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopProjectITI_int40.Models;

namespace TopProjectITI_int40.Repository.TeacherRepo.TeacherEductionRepositories
{
    public interface ITeacherEductionRepository
    {
        Task<QueryResult<TeacherEduction>> GetTeacherEductions(int teacherId); // 
        Task<TeacherEduction> GetTeacherEductionById(int teacherEductionId); //
        Task AddTeacherEduction(TeacherEduction teacherEduction);
        Task DeleteTeacherEduction(TeacherEduction teacherEduction);
    }
}
