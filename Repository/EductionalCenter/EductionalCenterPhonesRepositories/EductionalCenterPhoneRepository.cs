using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopProjectITI_int40.AppDBContext;
using TopProjectITI_int40.Models;

namespace TopProjectITI_int40.Repository.EductionalCenter.EductionalCenterPhonesRepositories
{
    public class EductionalCenterPhoneRepository :IEductionalCenterPhoneRepository
    {
        DBGProjectITI_Int40 _context;
        public EductionalCenterPhoneRepository(DBGProjectITI_Int40 context)
        {
            _context = context;
        }
        // get EductionalCenter phone by EductionalCenterId ,              // >> EductionalCenterId will come from techer login (token)
        public async Task<QueryResult<EductionalCenterPhone>> GetEductionalCenterPhones(int eductionalCenterId)
        {
            var result = new QueryResult<EductionalCenterPhone>();
            var query = _context.EductionalCenterPhones.Include(ec=>ec.EductionalCenter)
                .Where(a => a.EductionalCenterId == eductionalCenterId)
                .AsQueryable();
            result.Items = await query.ToListAsync();
            return result;
        }
        // CheckPhoneExistsMoreThanOne >> for check new phone exists one in table as one in case Add and 
        // also more than one in db in case Edit 
        public async Task<EductionalCenterPhone> CheckPhoneExists(string eductionalCenterPhoneNumber)
        {
            return await _context.EductionalCenterPhones.FirstOrDefaultAsync(a => a.EductionalCenterPhoneNumber == eductionalCenterPhoneNumber);
        }

        // search by id
        public async Task<EductionalCenterPhone> GetEductionalCenterPhoneById(int eductionalCenterPhoneId)
        {
            return await _context.EductionalCenterPhones.FindAsync(eductionalCenterPhoneId);
        }

        // Add NewPhone to EductionalCenterPhone
        public async Task AddEductionalCenterPhone(EductionalCenterPhone eductionalCenterPhone)
        {
            await _context.EductionalCenterPhones.AddAsync(eductionalCenterPhone);
            await _context.SaveChangesAsync();
        }
        //Edit EductionalCenter Phone
        public async Task EditEductionalCenterPhone(EductionalCenterPhone newEductionalCenterPhone)
        {
            EductionalCenterPhone oldEductionalCenterPhone = await GetEductionalCenterPhoneById(newEductionalCenterPhone.EductionalCenterPhoneId);
            oldEductionalCenterPhone.EductionalCenterPhoneNumber = newEductionalCenterPhone.EductionalCenterPhoneNumber;
            _context.Entry(oldEductionalCenterPhone).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        // delet EductionalCenterPhone
        public async Task DeleteEductionalCenterPhone(EductionalCenterPhone eductionalCenterPhone)
        {
            _context.EductionalCenterPhones.Remove(eductionalCenterPhone);
            await _context.SaveChangesAsync();
        }
    }
}
