using NJsonSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopProjectITI_int40.Models;

namespace TopProjectITI_int40.Repository.ParentRepo.ParentRepositories
{
    public interface IParentRepository
    {
        Task<IEnumerable<Parent>> GetParents();
        Task<Parent> GetParentById(int parentId);
        Task AddParent(Parent parent);
        Task EditParent(Parent parent, int parentId);
        Task DeleteParent(int parentId);
        Task<Parent> CheckParentLogin(string userName, string password);
    }
}
