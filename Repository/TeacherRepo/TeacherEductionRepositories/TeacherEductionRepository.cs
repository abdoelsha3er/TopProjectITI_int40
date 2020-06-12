using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopProjectITI_int40.AppDBContext;
using TopProjectITI_int40.Models;

namespace TopProjectITI_int40.Repository.TeacherRepo.TeacherEductionRepositories
{
    public class TeacherEductionRepository:ITeacherEductionRepository
    {
        DBGProjectITI_Int40 _context;
        public TeacherEductionRepository(DBGProjectITI_Int40 context)
        {
            _context = context;
        }
        // get  TeacherEduction by techerId ,              // >> TeacherId will come from techer login (token)
        public async Task<QueryResult<TeacherEduction>> GetTeacherEductions(int teacherId)
        {
            var result = new QueryResult<TeacherEduction>();
            var query = _context.TeacherEductions.Include(t => t.Teacher)
                .Where(a => a.TeacherId == teacherId)
                .AsQueryable();
            result.Items = await query.ToListAsync();
            return result;
        }
        // Get TeacherEduction by id
        public async Task<TeacherEduction> GetTeacherEductionById(int teacherEductionId)
        {
            return await _context.TeacherEductions.FindAsync(teacherEductionId);
        }
        // Add new TeacherEduction
        public async Task AddTeacherEduction(TeacherEduction teacherEduction)
        {
            await _context.TeacherEductions.AddAsync(teacherEduction);
            await _context.SaveChangesAsync();
        }
        // Delete TeacherEduction
        public async Task DeleteTeacherEduction(TeacherEduction teacherEduction)
        {
            _context.TeacherEductions.Remove(teacherEduction);
            await _context.SaveChangesAsync();
        }
    }
}
