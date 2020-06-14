using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopProjectITI_int40.AppDBContext;
using TopProjectITI_int40.Models;

namespace TopProjectITI_int40.Repository.EductionalCenter.EductionalCenterGroupRepositories
{
    public class EductionalCenterGroupRepository : IEductionalCenterGroupRepository
    {

        DBGProjectITI_Int40 _context;
        public EductionalCenterGroupRepository(DBGProjectITI_Int40 context)
        {
            _context = context;
        }

        // Add EductionalCenterGroup
        public async Task AddEductionalCenterGroup(EductionalCenterGroup eductionalCenterGroup)
        {
            await _context.EductionalCenterGroups.AddAsync(eductionalCenterGroup);
            await _context.SaveChangesAsync();
        }

        // Search By eductionalCenterGroupId
        public async Task<EductionalCenterGroup> GetEductionalCenterGroupById(int eductionalCenterGroupId)
        {
            return await _context.EductionalCenterGroups.FindAsync(eductionalCenterGroupId);
        }

        // Edit
        public async Task EditEductionalCenterGroup(EductionalCenterGroup newEductionalCenterGroup, int eductionalCenterGroupId)  // for editing profile
        {
            EductionalCenterGroup oldEductionalCenterGroup = await GetEductionalCenterGroupById(eductionalCenterGroupId);
            oldEductionalCenterGroup.Name = newEductionalCenterGroup.Name;
            oldEductionalCenterGroup.TeacherId = newEductionalCenterGroup.TeacherId;
            oldEductionalCenterGroup.SubjectId = newEductionalCenterGroup.SubjectId;
            oldEductionalCenterGroup.GradeId = newEductionalCenterGroup.GradeId;
            oldEductionalCenterGroup.Description = newEductionalCenterGroup.Description;
            oldEductionalCenterGroup.TotleStudents = newEductionalCenterGroup.TotleStudents;
            oldEductionalCenterGroup.DateFrom = newEductionalCenterGroup.DateFrom;
            oldEductionalCenterGroup.DateTo = newEductionalCenterGroup.DateTo;
            oldEductionalCenterGroup.PriceInMonth = newEductionalCenterGroup.PriceInMonth;
            oldEductionalCenterGroup.Status = newEductionalCenterGroup.Status;
            oldEductionalCenterGroup.ArchivedReason = newEductionalCenterGroup.ArchivedReason;
            //_context.Parents.Update(oldEductionalCenterGroup);
            _context.Entry(oldEductionalCenterGroup).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        // delete by id
        public async Task DeleteEductionalCenterGroup(int eductionalCenterGroupId)
        {
            var eductionalCenterGroup = await GetEductionalCenterGroupById(eductionalCenterGroupId);
            _context.EductionalCenterGroups.Remove(eductionalCenterGroup);
            await _context.SaveChangesAsync();
        }

    }
}
