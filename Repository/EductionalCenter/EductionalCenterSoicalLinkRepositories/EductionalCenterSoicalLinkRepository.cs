using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopProjectITI_int40.AppDBContext;
using TopProjectITI_int40.Models;

namespace TopProjectITI_int40.Repository.EductionalCenter.EductionalCenterSoicalLinkRepositories
{
    public class EductionalCenterSoicalLinkRepository : IEductionalCenterSoicalLinkRepository
    {
        DBGProjectITI_Int40 _context;
        public EductionalCenterSoicalLinkRepository(DBGProjectITI_Int40 context)
        {
            _context = context;
        }
        // get  EductionalCenterSoicalLink by EductionalCenterId ,              // >> TeacherId will come from techer login (token)
        public async Task<QueryResult<EductionalCenterSoicalLink>> GetEductionalCenterSoicalLinks(int eductionalCenterId)
        {
            var result = new QueryResult<EductionalCenterSoicalLink>();
            var query = _context.EductionalCenterSoicalLinks.Include(ec => ec.EductionalCenter)
                .Where(a => a.EductionalCenterSoicalLinkId == eductionalCenterId)
                .AsQueryable();
            result.Items = await query.ToListAsync();
            return result;
        }
        // Get EductionalCenterSoicalLink by id
        public async Task<EductionalCenterSoicalLink> GetEductionalCenterSoicalLinkById(int eductionalCenterSoicalLinkId)
        {
            return await _context.EductionalCenterSoicalLinks.FindAsync(eductionalCenterSoicalLinkId);
        }
        // Add new EductionalCenter SocialLink
        public async Task AddEductionalCenterSoicalLink(EductionalCenterSoicalLink eductionalCenterSoicalLink)
        {
            await _context.EductionalCenterSoicalLinks.AddAsync(eductionalCenterSoicalLink);
            await _context.SaveChangesAsync();
        }
        // Delete EductionalCenter SocialLink
        public async Task DeleteEductionalCenterSoicalLink(EductionalCenterSoicalLink eductionalCenterSoicalLink)
        {
            _context.EductionalCenterSoicalLinks.Remove(eductionalCenterSoicalLink);
            await _context.SaveChangesAsync();
        }
    }
}
