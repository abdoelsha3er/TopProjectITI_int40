using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopProjectITI_int40.AppDBContext;
using TopProjectITI_int40.Models;

namespace TopProjectITI_int40.Repository.TeacherRepo.TeacherSocialLinkRepositories
{
    public class TeacherSocialLinkRepository :ITeacherSocialLinkRepository
    {
        DBGProjectITI_Int40 _context;
        public TeacherSocialLinkRepository(DBGProjectITI_Int40 context)
        {
            _context = context;
        }
        // get  Teacher SocialLink by techerId ,              // >> TeacherId will come from techer login (token)
        public async Task<QueryResult<TeacherSocialLink>> GetTeacherSocialLinks(int teacherId)
        {
            var result = new QueryResult<TeacherSocialLink>();
            var query = _context.TeacherSocialLinks.Include(t => t.Teacher)
                .Where(a => a.TeacherId == teacherId)
                .AsQueryable();
            result.Items = await query.ToListAsync();
            return result;
        }
        // Get teacher SocialLink by id
        public async Task<TeacherSocialLink> GetTeacherSocialLinkById(int teacherSocialLinkId)
        {
            return await _context.TeacherSocialLinks.FindAsync(teacherSocialLinkId);
        }
        // Add new Teacher SocialLink
        public async Task AddTeacherSocialLink(TeacherSocialLink teacherSocialLink)
        {
            await _context.TeacherSocialLinks.AddAsync(teacherSocialLink);
            await _context.SaveChangesAsync();
        }
        // Delete Teacher SocialLink
        public async Task DeleteTeacherSocialLink(TeacherSocialLink teacherSocialLink)
        {
            _context.TeacherSocialLinks.Remove(teacherSocialLink);
            await _context.SaveChangesAsync();
        }
    }
}
