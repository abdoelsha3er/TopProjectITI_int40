using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopProjectITI_int40.Models;

namespace TopProjectITI_int40.Repository.ReportRepo.ReportReporitories.ReportSupRepositories
{
    public interface IReportSupRepository
    {
        Task<int> AddReport(string NextLecture, string Homework);
        Task<List<Report>> GetAllReportToCenter(int groupid);
    }
}
