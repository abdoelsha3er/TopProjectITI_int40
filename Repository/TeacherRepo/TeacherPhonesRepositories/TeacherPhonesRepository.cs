using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopProjectITI_int40.AppDBContext;
using TopProjectITI_int40.Models;

namespace TopProjectITI_int40.Repository.TeacherRepo.TeacherPhonesRepositories
{
    public class TeacherPhonesRepository : ITeacherPhonesRepository
    {
        DBGProjectITI_Int40 _context;
        public TeacherPhonesRepository(DBGProjectITI_Int40 context)
        {
            _context = context;
        }
        // get techer phone by techerId ,              // >> TeacherId will come from techer login (token)
        public async Task<QueryResult<TeacherPhone>> GetTeacherPhones(int teacherId)
        {
            var result = new QueryResult<TeacherPhone>();
            var query = _context.TeacherPhones.Include(t => t.Teacher)
                .Where(a => a.TeacherId == teacherId)
                .AsQueryable();
            result.Items = await query.ToListAsync();
            return result;
        }

        // CheckPhoneExistsMoreThanOne >> for check new phone exists one in table as one in case Add and 
        // also more than one in db in case Edit 
        public async Task<TeacherPhone> CheckPhoneExists(string teacherPhoneNumber)
        {
            return await _context.TeacherPhones.FirstOrDefaultAsync(a => a.TeacherPhoneNumber == teacherPhoneNumber);
        }

        // search by id
        public async Task<TeacherPhone> GetTeacherPhoneById(int teacherPhoneId)
        {
            return await _context.TeacherPhones.FindAsync(teacherPhoneId);
        }
        // Add NewPhone to teacher
        public async Task AddTeacherPhone(TeacherPhone teacherPhone)
        {
            await _context.TeacherPhones.AddAsync(teacherPhone);
            await _context.SaveChangesAsync();
        }       
        //Edit Teacher Phone
        public async Task EditTeacherPhone(TeacherPhone newTeacherPhone)
        {
            TeacherPhone oldTeacherPhone = await GetTeacherPhoneById(newTeacherPhone.TeacherPhoneId);
            oldTeacherPhone.TeacherPhoneNumber = newTeacherPhone.TeacherPhoneNumber;
            _context.Entry(oldTeacherPhone).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        // delet TeacherPhone
        public async Task DeleteTeacherPhone(TeacherPhone teacherPhone)
        {
            _context.TeacherPhones.Remove(teacherPhone);
            await _context.SaveChangesAsync();
        }
    }
}
