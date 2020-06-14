using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TopProjectITI_int40.Models;

namespace TopProjectITI_int40.Repository.EductionalCenter.EductionalCenterGroupRepositories
{
    public interface IEductionalCenterGroupRepository
    {
        Task AddEductionalCenterGroup(EductionalCenterGroup eductionalCenterGroup);
        Task<EductionalCenterGroup> GetEductionalCenterGroupById(int eductionalCenterGroupId);   // eductionalcentergroup
        Task EditEductionalCenterGroup(EductionalCenterGroup eductionalCenterGroup, int eductionalCenterGroupId);
        Task DeleteEductionalCenterGroup(int eductionalCenterGroupId);
    }
}
