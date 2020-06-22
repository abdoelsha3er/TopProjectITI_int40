using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TopProjectITI_int40.Models;

namespace TopProjectITI_int40.Repository.EductionalCenterRepo.EductionalCenterGroupRepositories
{
    public interface IEductionalCenterGroupRepository
    {
        Task<IEnumerable<EductionalCenterGroup>> GetEductionalCenterGroups(int eductionalCenterId);
        Task<IEnumerable<EductionalCenterGroup>> GetGroupsByCenterIDGradeIdSubjectID(int eductionalCenterGroupId, int gradeId, int subjectid);
        Task AddEductionalCenterGroup(EductionalCenterGroup eductionalCenterGroup);
        Task<EductionalCenterGroup> GetEductionalCenterGroupById(int eductionalCenterGroupId);   // eductionalcentergroup
        Task EditEductionalCenterGroup(EductionalCenterGroup eductionalCenterGroup, int eductionalCenterGroupId);
        Task DeleteEductionalCenterGroup(int eductionalCenterGroupId);
    }
}
