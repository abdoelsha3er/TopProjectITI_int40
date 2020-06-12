using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopProjectITI_int40.Models;

namespace TopProjectITI_int40.Repository.EductionalCenter.EductionalCenterPhonesRepositories
{
    public interface IEductionalCenterPhoneRepository
    {
        Task<QueryResult<EductionalCenterPhone>> GetEductionalCenterPhones(int eductionalCenterId);
        Task<EductionalCenterPhone> CheckPhoneExists(string eductionalCenterPhoneNumber);   // when add operation check number exists with another or no
        Task<EductionalCenterPhone> GetEductionalCenterPhoneById(int eductionalCenterPhoneId);       // search by id
        Task AddEductionalCenterPhone(EductionalCenterPhone eductionalCenterPhone);
        Task EditEductionalCenterPhone(EductionalCenterPhone newEductionalCenterPhone);
        Task DeleteEductionalCenterPhone(EductionalCenterPhone eductionalCenterPhone);
    }
}
