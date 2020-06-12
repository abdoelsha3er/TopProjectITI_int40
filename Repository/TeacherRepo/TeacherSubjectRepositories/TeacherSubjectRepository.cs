using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopProjectITI_int40.AppDBContext;
using TopProjectITI_int40.Models;

namespace TopProjectITI_int40.Repository.TeacherRepo.TeacherSubjectRepositories
{
    public class TeacherSubjectRepository : ITeacherSubjectRepository
    {
        DBGProjectITI_Int40 _context;
        public TeacherSubjectRepository(DBGProjectITI_Int40 context)
        {
            _context = context;
        }

        // Get All Subjects that not assign to this teacher
        public async Task<QueryResult<TeacherSubjects>> GetTeacherSubjectsNotAssign(int teacherId, int csid)
        {
            var result = new QueryResult<TeacherSubjects>();
            var query = _context.TeacherSubjects.Include(s => s.Subject).ThenInclude(c => c.CategorySubject)
                .Where(a => a.TeacherId != teacherId)
                .Where(a => a.Subject.CategorySubjectId == csid)
                .AsQueryable();
            result.Items = await query.ToListAsync();
            return result;
        }
        // Get All Subjects that not assign to this teacher
        //public async Task<QueryResult<TeacherSubjects>> GetTeacherSubjectsNotAssign(int teacherId)
        //{
        //    var result = new QueryResult<TeacherSubjects>();
        //    //return await _context.TeacherSubjects.Include(s => s.Subject).ToArrayAsync(a => a.TeacherId != TeacherId);
        //    //var results = await (from n in _context.TeacherSubjects.Include(s => s.Subject) where n.TeacherId == TeacherId select n).ToListAsync();
        //    //return results;
        //    //return await _context.TeacherSubjects.Include(s => s.Subject).Where(a => a.TeacherId == TeacherId).ToListAsync();
        //    var query = _context.TeacherSubjects.Include(s => s.Subject).Where(a => a.TeacherId == teacherId).AsQueryable();
        //    result.Items = await query.ToListAsync();
        //    return result;
        //}

        //Subject GetTeacherSubjectstById(int id); // TeacherId will Selected by teacher login (token) // 
        public async Task<QueryResult<TeacherSubjects>> GetTeacherSubjectst(int teacherId)
        {
            var result = new QueryResult<TeacherSubjects>();
            var query = _context.TeacherSubjects.Include(s => s.Subject)
                .Where(a => a.TeacherId == teacherId)
                .AsQueryable();
            result.Items = await query.ToListAsync();
            return result;
        }
        // Add new TeacherSubject
        public async Task AddTeacherSubject(TeacherSubjects teacherSubject)
        {
            await _context.TeacherSubjects.AddAsync(teacherSubject);
            await _context.SaveChangesAsync();
        }
        // remove TeacherSubject
        public async Task DeleteTeacherSubject(TeacherSubjects teacherSubject)
        {
            _context.TeacherSubjects.Remove(teacherSubject);
            await _context.SaveChangesAsync();
        }
    }
}
