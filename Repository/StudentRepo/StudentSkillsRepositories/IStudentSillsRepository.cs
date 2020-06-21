using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopProjectITI_int40.Models;

namespace TopProjectITI_int40.Repository.StudentRepo.StudentSkillsRepositories
{
   public interface IStudentSillsRepository
    {
        Task<IEnumerable<StudentSkill>> GetStudentSkills(int studentId);
        Task<StudentSkill> GetStudentSkillsById(int StudentSkillId);
        Task AddStudentSkill(StudentSkill StudentSkill);
    }
}
