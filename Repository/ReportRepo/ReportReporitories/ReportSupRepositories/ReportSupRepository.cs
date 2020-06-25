using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopProjectITI_int40.AppDBContext;
using TopProjectITI_int40.Models;

namespace TopProjectITI_int40.Repository.ReportRepo.ReportReporitories.ReportSupRepositories
{
    public class ReportSupRepository : IReportSupRepository
    {
        DBGProjectITI_Int40 _context;

        public ReportSupRepository(DBGProjectITI_Int40 context)
        {
            _context = context;
        }
        public async Task<int> AddReport(string NextLecture, string Homework)
        {
            Report _report = new Report();
            _report.NextLecture = NextLecture;
            _report.Homework = Homework;
            _report.reportDate = DateTime.Now;

            await _context.Reports.AddAsync(_report);
            await _context.SaveChangesAsync();
            return _report.ReportId;
        }
        public async Task<List<Report>> GetAllReportToCenter(int groupid)
        {
            List<Report> reports = new List<Report>();
            List<ReportStudentGroup> reportsstudentgroup = await _context.ReportStudentGroups.Where(a => a.EductionalCenterGroupId == groupid).ToListAsync();
            for (int i = 0; i < reportsstudentgroup.Count; i++)
            {
                Report report = await _context.Reports.FirstOrDefaultAsync(s => s.ReportId == reportsstudentgroup[i].ReportId);
                if (!reports.Contains(report))
                {
                    reports.Add(report);
                }
            }
            return reports;
        }
    }
}
