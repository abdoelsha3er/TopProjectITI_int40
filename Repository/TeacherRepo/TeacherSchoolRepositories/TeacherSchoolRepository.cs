using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopProjectITI_int40.AppDBContext;
using TopProjectITI_int40.Models;

namespace TopProjectITI_int40.Repository.TeacherRepo.TeacherSchoolRepositories
{
    public class TeacherSchoolRepository : ITeacherSchoolRepository
    {
        DBGProjectITI_Int40 _context;
        public TeacherSchoolRepository(DBGProjectITI_Int40 context)
        {
            _context = context;
        }
        // get  Teacher TeacherSchools by techerId ,              // >> TeacherId will come from techer login (token)
        public async Task<QueryResult<TeacherSchool>> GetTeacherSchools(int teacherId) // paga 
        {
            var result = new QueryResult<TeacherSchool>();
            var query = _context.TeacherSchools.Include(t => t.Teacher)
                .Where(a => a.TeacherId == teacherId)
                .AsQueryable();
            result.Items = await query.ToListAsync();
            return result;
        }
        // Get teacher TeacherSchool by id
        public async Task<TeacherSchool> GetTeacherSchoolById(int teacherSchoolId)
        {
            return await _context.TeacherSchools.FindAsync(teacherSchoolId);
        }
        // Add new TeacherSchool
        public async Task AddTeacherSchool(TeacherSchool teacherSchool)
        {
            await _context.TeacherSchools.AddAsync(teacherSchool);
            await _context.SaveChangesAsync();
        }
        // Delete TeacherSchool
        public async Task DeleteTeacherSchool(TeacherSchool teacherSchool)
        {
            _context.TeacherSchools.Remove(teacherSchool);
            await _context.SaveChangesAsync();
        }
    }
}
