using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopProjectITI_int40.Models;

namespace TopProjectITI_int40.Repository.TeacherRepo.TeacherExperienceRepositories
{
    public interface ITeacherExperienceRepository
    {
        Task<QueryResult<TeacherExperience>> GetTeacherExperience(int teacherId);
        Task<TeacherExperience> GetTeacherExperienceById(int teacherExperienceId);   // get by id
        //Task<QueryResult<TeacherPhone>> CheckPhoneExistsMoreThanOne(string teacherPhoneNumber);
        //Task<TeacherExperience> CheckPhoneExists(string teacherExperience, int teacherId);   // when add operation check number exists with another or no
        Task AddTeacherExperience(TeacherExperience teacherExperience);
        Task EditTeacherExperience(TeacherExperience newTeacherExperience);
        Task DeleteTeacherExperience(TeacherExperience teacherExperience);
    }
}
