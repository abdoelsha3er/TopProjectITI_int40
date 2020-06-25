using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopProjectITI_int40.AppDBContext;
using TopProjectITI_int40.Models;

namespace TopProjectITI_int40.Repository.ReportRepo.ReportReporitories
{
    public class ReportRepository : IReportRepository
    {
        DBGProjectITI_Int40 _context;
        public ReportRepository(DBGProjectITI_Int40 context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ReportStudentGroup>> AddAllReportsToParent(ReportStudentGroup[] reports, int reportId)
        {
            foreach (var item in reports)
            {
                item.ReportId = reportId;
                await _context.ReportStudentGroups.AddAsync(item);
                await _context.SaveChangesAsync();
            }
            return reports;
        }

        public async Task<IEnumerable<ReportStudentGroup>> GetAllReportsToCenter(int groupId, DateTime date)
        {
            List<Report> reportsWithdate = await _context.Reports.Where(a => a.reportDate == date).ToListAsync();
            foreach (var item in reportsWithdate)
            {
                return await _context.ReportStudentGroups.Where(s => s.studentGroup.EductionalCenterGroupId == groupId
                && item.ReportId == s.ReportId).ToListAsync();
            }
            return null;
        }

        public async Task<IEnumerable<ReportStudentGroup>> GetAllReportsToParentInGroup(int studentId, int groupId)
        {
            return await _context.ReportStudentGroups.Include(r => r.Report)
                .Where(a => a.studentGroup.StudentId == studentId && a.studentGroup.EductionalCenterGroupId == groupId)
                .ToListAsync();
        }

        public async Task<ReportStudentGroup> GetReportToParent(int studentId, int groupId, DateTime date)
        {
            List<Report> reportsWithdate = await _context.Reports.Where(a => a.reportDate == date).ToListAsync();
            // Task<IEnumerable<ReportStudentGroup>> reportstudentgroup = GetAllReportsToParentInGroup(studentId, groupId);
            foreach (var item in reportsWithdate)
            {
                return await _context.ReportStudentGroups.FirstOrDefaultAsync(s => s.ReportId == item.ReportId && s.studentGroup.StudentId == studentId
                && s.studentGroup.EductionalCenterGroupId == groupId);
            }
            return null;
        }
    }
}