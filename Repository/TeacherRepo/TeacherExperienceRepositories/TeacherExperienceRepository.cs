using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopProjectITI_int40.AppDBContext;
using TopProjectITI_int40.Models;

namespace TopProjectITI_int40.Repository.TeacherRepo.TeacherExperienceRepositories
{
    public class TeacherExperienceRepository : ITeacherExperienceRepository
    {
        DBGProjectITI_Int40 _context;
        public TeacherExperienceRepository(DBGProjectITI_Int40 context)
        {
            _context = context;
        }
        // get  Teacher Experiences by techerId ,              // >> TeacherId will come from techer login (token)
        public async Task<QueryResult<TeacherExperience>> GetTeacherExperience(int teacherId)
        {
            var result = new QueryResult<TeacherExperience>();
            var query = _context.TeacherExperiences.Include(t => t.Teacher)
                .Where(a => a.TeacherId == teacherId)
                .AsQueryable();
            result.Items = await query.ToListAsync();
            return result;
        }
        // Get teacher Experience by id
        public async Task<TeacherExperience> GetTeacherExperienceById(int teacherExperienceId)
        {
            return await _context.TeacherExperiences.FindAsync(teacherExperienceId);
        }

        // Add new Teacher Experience
        public async Task AddTeacherExperience(TeacherExperience teacherExperience)
        {
            await _context.TeacherExperiences.AddAsync(teacherExperience);
            await _context.SaveChangesAsync();
        }

        //Edit Teacher Phone
        public async Task EditTeacherExperience(TeacherExperience newTeacherExperience)
        {
            TeacherExperience oldTeacherExperience = await _context.TeacherExperiences.FindAsync(newTeacherExperience.TeacherExperienceId);
            oldTeacherExperience.Description = newTeacherExperience.Description;
            oldTeacherExperience.StartDate = newTeacherExperience.StartDate;
            oldTeacherExperience.EndDate = newTeacherExperience.EndDate;
            _context.Entry(oldTeacherExperience).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        // Delete Teacher Experience
        public async Task DeleteTeacherExperience(TeacherExperience teacherExperience)
        {
            _context.TeacherExperiences.Remove(teacherExperience);
            await _context.SaveChangesAsync();
        }
    }
}
