using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopProjectITI_int40.AppDBContext;
using TopProjectITI_int40.Models;

namespace TopProjectITI_int40.Repository.StudentRepo.StudentSkillsRepositories
{
    public class StudentSkillsRepository : IStudentSillsRepository
    {
        DBGProjectITI_Int40 _context;
        public StudentSkillsRepository(DBGProjectITI_Int40 context)
        {
            _context = context;
        }
        public async Task AddStudentSkill(StudentSkill studentSkill)
        {
            await _context.StudentSkills.AddAsync(studentSkill);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<StudentSkill>> GetStudentSkills(int studentId)
        {
            //var result = new QueryResult<StudentSkills>();
            //var query = _context.StudentSkills.Include(t => t.Student)
            //    .Where(a => a.StudentId == studentId)
            //    .AsQueryable();
            //result.Items = await query.ToListAsync();
            //return result;

            return await _context.StudentSkills.Where(a => a.StudentId == studentId).ToListAsync();
        }

        public async Task<StudentSkill> GetStudentSkillsById(int StudentSkillId)
        {
            return await _context.StudentSkills.FindAsync(StudentSkillId);
        }
    }
}
