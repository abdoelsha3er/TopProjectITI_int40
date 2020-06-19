using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopProjectITI_int40.Models;

namespace TopProjectITI_int40.Repository.EductionalCenterRepo.EductionalCenterSubjectsRepositories
{
    public interface IEductionalCenterSubjectRepository
    {
        // Get All Subjects that not assign to this EductionalCenter        //eductionalCenterId >> token when login
        //Task<QueryResult<EductionalCenterSubjects>> GetEductionalCenterSubjecsAssign(int eductionalCenterId);

        Task<IEnumerable<EductionalCenterSubjects>> GetEductionalCenterSubjecsAssign(int eductionalCenterId);
        Task AddEductionalCenterSubject(EductionalCenterSubjects eductionalCenterSubject);
        Task DeleteEductionalCenterSubject(EductionalCenterSubjects eductionalCenterSubject);


        //Task<QueryResult<EductionalCenterSubjects>> GetTeacherSubjectsNotAssign2(int ecId, int csid);
        //Task<QueryResult<EductionalCenterSubjects>> GetEductionalCenterSubjectsNotAssign(int eductionalCenterId, int categorySubjectId); 
        //Task<QueryResult<EductionalCenterSubjects>> GetEductionalCenterSubjects(int eductionalCenterId);
        //Task Add<EductionalCenterSubject>(EductionalCenterSubjects eductionalCenterSubject);
        //Task DeleteEductionalCenter(EductionalCenterSubjects eductionalCenterSubject);
    }
}
