using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopProjectITI_int40.Models;

namespace TopProjectITI_int40.Repository.TeacherRepo.TeacherPhonesRepositories
{
    public interface ITeacherPhonesRepository
    {
        Task<QueryResult<TeacherPhone>> GetTeacherPhones(int teacherId);
        //Task<QueryResult<TeacherPhone>> CheckPhoneExistsMoreThanOne(string teacherPhoneNumber);
        Task<TeacherPhone> CheckPhoneExists(string teacherPhoneNumber);   // when add operation check number exists with another or no
        Task<TeacherPhone> GetTeacherPhoneById(int teacherPhoneId);       // search by id
        Task AddTeacherPhone(TeacherPhone teacherPhone);
        Task EditTeacherPhone(TeacherPhone newTeacherPhone);
        Task DeleteTeacherPhone(TeacherPhone teacherPhone);
    }
}
