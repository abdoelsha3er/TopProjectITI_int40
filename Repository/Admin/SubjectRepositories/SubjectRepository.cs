using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopProjectITI_int40.AppDBContext;
using TopProjectITI_int40.Models;

namespace TopProjectITI_int40.Repository.Admin.SubjectRepositories
{
    public class SubjectRepository : ISubjectRepository
    {
        DBGProjectITI_Int40 _context;
        public SubjectRepository(DBGProjectITI_Int40 context)
        {
            _context = context;
        }
        // get All Subjects
        public async Task<IEnumerable<Subject>> GetSubjects()
        {
            return await _context.Subjects.Include(c => c.CategorySubject).ToListAsync();
        }
        //Get Subject By id
        public async Task<Subject> GetSubjectById(int id)
        {
            return await _context.Subjects.Include(c => c.CategorySubject).FirstOrDefaultAsync(a => a.SubjectId == id);
        }
        // Add
        public async Task AddSubject(Subject subject)
        {
            await _context.Subjects.AddAsync(subject);
            await _context.SaveChangesAsync();
        }
        //Edit
        public async Task EditSubject(Subject newSubject)
        {
            Subject oldsubject = await _context.Subjects.FindAsync(newSubject.SubjectId);
            oldsubject.Name = newSubject.Name;
            oldsubject.CategorySubjectId = newSubject.CategorySubjectId;
            //_context.Subjects.Update(oldsubject);
            _context.Entry(oldsubject).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        //Delete
        public async Task DeleteSubject(Subject subject)
        {
            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();
        }
        //get by category id
        public async Task<QueryResult<Subject>> GetByCategoryId(int Catid)
        {
            var result = new QueryResult<Subject>();
            var query = _context.Subjects.Include(s => s.CategorySubject)
                .Where(a => a.CategorySubjectId == Catid).AsQueryable();
            //.Where(a => a.Subject.CategorySubjectId == categorySubjectId)

            result.Items = await query.ToListAsync();
            return result;


        }
    }
}
