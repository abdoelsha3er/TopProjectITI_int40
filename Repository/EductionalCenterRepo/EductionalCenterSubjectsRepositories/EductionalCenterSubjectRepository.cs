using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopProjectITI_int40.AppDBContext;
using TopProjectITI_int40.Models;

namespace TopProjectITI_int40.Repository.EductionalCenterRepo.EductionalCenterSubjectsRepositories
{
    public class EductionalCenterSubjectRepository : IEductionalCenterSubjectRepository
    {
        DBGProjectITI_Int40 _context;
        public EductionalCenterSubjectRepository(DBGProjectITI_Int40 context)
        {
            _context = context;
        }
        ////Get All Subjects that assign to this Eductional Center
        //public async Task<QueryResult<EductionalCenterSubjects>> GetEductionalCenterSubjecsAssign(int eductionalCenterId)
        //{
        //    var result = new QueryResult<EductionalCenterSubjects>();
        //    var query = _context.EductionalCenterSubjects.Include(s => s.Subject).ThenInclude(c => c.CategorySubject)
        //                .Where(a => a.EductionalCenterId == eductionalCenterId).AsQueryable();
        //    result.Items = await query.ToListAsync();
        //    return result;
        //}

        //Get All Subjects that assign to this Eductional Center
        public async Task<IEnumerable<EductionalCenterSubjects>> GetEductionalCenterSubjecsAssign(int eductionalCenterId)
        {
            return await _context.EductionalCenterSubjects.Include(c => c.EductionalCenter)
                                .Include(s => s.Subject)
                                .Where(a=>a.EductionalCenterId==eductionalCenterId).ToListAsync();
        }

        // Get by id id

        public async Task<EductionalCenterSubjects> GetEductionalCenterSubject(int subjectId, int eductionalCenterId)
        {
            return await _context.EductionalCenterSubjects.FirstOrDefaultAsync(es => es.SubjectId == subjectId&& es.EductionalCenterId == eductionalCenterId);
        }

        // Add new EductionalCenterSubject to EductionalCenter
        public async Task AddEductionalCenterSubject( EductionalCenterSubjects eductionalCenterSubject)
        {
            await _context.EductionalCenterSubjects.AddAsync(eductionalCenterSubject);
            await _context.SaveChangesAsync();
        }
        // remove eductionalCenterSubject
        public async Task DeleteEductionalCenterSubject(EductionalCenterSubjects eductionalCenterSubject)
        {
            _context.EductionalCenterSubjects.Remove(eductionalCenterSubject);
            await _context.SaveChangesAsync();
        }
    }
}
