using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopProjectITI_int40.Models;

namespace TopProjectITI_int40.Repository.EductionalCenterRepo.EductionalCenterSoicalLinkRepositories
{
    public interface IEductionalCenterSoicalLinkRepository
    {
        Task<QueryResult<EductionalCenterSoicalLink>> GetEductionalCenterSoicalLinks(int eductionalCenterId); // 
        Task<EductionalCenterSoicalLink> GetEductionalCenterSoicalLinkById(int teacherSocialLinkId); //
        Task AddEductionalCenterSoicalLink(EductionalCenterSoicalLink eductionalCenterSoicalLink);
        Task DeleteEductionalCenterSoicalLink(EductionalCenterSoicalLink eductionalCenterSoicalLink);
    }
}
