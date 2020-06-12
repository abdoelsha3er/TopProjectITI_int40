using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopProjectITI_int40.Models;

namespace TopProjectITI_int40.Repository.Admin.SubjectRepositories
{
    public interface ISubjectRepository
    {
        Task<IEnumerable<Subject>> GetSubjects();
        Task<Subject> GetSubjectById(int id);
        //Subject GetSubjectByIdd(int id);
        Task<QueryResult<Subject>> GetByCategoryId(int id);
        Task AddSubject(Subject subject);
        Task EditSubject(Subject newSubject);
        Task DeleteSubject(Subject subject);
        //Task<Subject> GetDetailsCategorySubjectById(int id);
    }
}
