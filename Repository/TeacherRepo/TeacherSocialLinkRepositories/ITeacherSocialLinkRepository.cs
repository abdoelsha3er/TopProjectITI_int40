using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopProjectITI_int40.Models;

namespace TopProjectITI_int40.Repository.TeacherRepo.TeacherSocialLinkRepositories
{
    public interface ITeacherSocialLinkRepository
    {
        Task<QueryResult<TeacherSocialLink>> GetTeacherSocialLinks(int teacherId); // 
        Task<TeacherSocialLink> GetTeacherSocialLinkById(int teacherSocialLinkId); //
        Task AddTeacherSocialLink(TeacherSocialLink teacherSocialLink);
        Task DeleteTeacherSocialLink(TeacherSocialLink teacherSocialLink);
    }
}
