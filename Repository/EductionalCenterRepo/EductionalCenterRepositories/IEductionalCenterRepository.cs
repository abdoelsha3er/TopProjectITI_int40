using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopProjectITI_int40.Models;

namespace TopProjectITI_int40.Repository.EductionalCenterRepo.EductionalCenterRepositories
{
    public interface IEductionalCenterRepository
    {
        //Task<IEnumerable<EductionalCenter>> GetEductionalCenters(int eductionalCenterId);
        Task AddEductionalCenter(EductionalCenter eductionalCenter);          // id will get by id
        Task<IEnumerable<EductionalCenter>> GetEductionalCenters();
        Task<EductionalCenter> GetEductionalCenterById(int eductionalCenterId);
        Task<EductionalCenter> GetEductionalCenterByUserName(string userName);
        Task EditEductionalCenter(EductionalCenter newEductionalCenter, int eductionalCenterId);
    }
}
