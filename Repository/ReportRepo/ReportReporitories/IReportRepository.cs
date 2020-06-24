using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopProjectITI_int40.Models;

namespace TopProjectITI_int40.Repository.ReportRepo.ReportReporitories
{
    public interface IReportRepository
    {
        Task<IEnumerable<ReportStudentGroup>> GetAllReportsToCenter(int groupId, DateTime date);
        Task<ReportStudentGroup> GetReportToParent(int studentId, int groupId, DateTime date);

        Task<IEnumerable<ReportStudentGroup>> GetAllReportsToParentInGroup(int studentId, int groupId);
        Task<IEnumerable<ReportStudentGroup>> AddAllReportsToParent(ReportStudentGroup[] reports);
    }
}
